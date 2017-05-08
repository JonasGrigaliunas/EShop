using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EShop.Data;
using EShop.Readers;
using EShop.Areas.Warehouse.Models.ShipmentViewModel;
using EShop.Handlers.Shipment;

namespace EShop.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ShipmentController : Controller
    {
        private readonly IShipmentReader _shipmentReader;
        private readonly ICreateShipmentHandler _createShipmentHandler;
        private readonly IEditShipmentHandler _editShipmentHandler;
        private readonly IItemListHandler _itemListHandler;

        private readonly IItemReader _itemReader;

        private readonly IReader<ShipmentItem> _shipmentItemReader;

        public ShipmentController(IShipmentReader shipmentReader, ICreateShipmentHandler createShipmentHandler, IEditShipmentHandler editShipmentHandler,
            IItemReader itemReader, IItemListHandler itemListHandler, IReader<ShipmentItem> shipmentItemReader)
        {
            _shipmentReader = shipmentReader;
            _createShipmentHandler = createShipmentHandler;
            _editShipmentHandler = editShipmentHandler;

            _itemReader = itemReader;
            _itemListHandler = itemListHandler;

            _shipmentItemReader = shipmentItemReader;
        }

        public IActionResult Index(string searchBoxInput)
        {
            var shipments = _shipmentReader
                .ByNumber(searchBoxInput)
                .OrderBy(x => x.Number)
                .ToList();

            return View(shipments);
        }


        // GET: Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Create

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CreateShipment shipment)
        {
            if (ModelState.IsValid)
            {
                _createShipmentHandler.Handle(shipment);
                return RedirectToAction("Index");
            }
            else
            {
                return View(shipment);
            }
        }


        public IActionResult Edit(int? id, string searchBoxInput)
        {
            if (!id.HasValue)
            {
                RedirectToAction("Index", "Shipment");
            }

            var allItems = _itemReader
                .ByCode(searchBoxInput)
                .ToList();
            

            var chosenShipment = _shipmentReader.Get(id.Value);

            //Already included using reader include????
            //Include items with shipment items
            var items = _shipmentItemReader.GetAll().Where(x => x.ShipmentId == id.Value).ToList();

            // Nesuprantu, kodel neveikia butent ShipmentItem pavertimas i View klase (Items)
            // Nebera to kodo, kuris anksciau uzkraudavo ShipmentItem'us (ne Include)

            var shipmentDetails = new EditShipment()
            {
                Id = chosenShipment.Id,
                From = chosenShipment.From,
                Number = chosenShipment.Number,

                AllItems = _itemListHandler.ToSelectableList(allItems),
                Items = _itemListHandler.ToSelectableList(items)

            };
            return View(shipmentDetails);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditShipment shipment)
        {
            if (ModelState.IsValid)
            {
                _editShipmentHandler.Handle(shipment);
                return RedirectToAction("Index");
            }
            else
            {
                return View(shipment);
            }
        }

        [HttpPost]
        public IActionResult EditShipmentItems(EditShipment selectedItems, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    _itemListHandler.AddItems(selectedItems, id.Value);
                    return RedirectToAction("Edit", "Shipment", new { id = id.Value });
                }
                // Redirect for good model, bad id
                return RedirectToAction("Index");
            }
            else
            {
                //return form instead of redirect
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult RemoveShipmentItems(EditShipment selectedItems, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    _itemListHandler.RemoveItems(selectedItems, id.Value);
                    return RedirectToAction("Edit", "Shipment", new { id = id.Value });
                }
                // Redirect for good model, bad id
                return RedirectToAction("Index");
            }
            else
            {
                //return form instead of redirect
                return RedirectToAction("Index");
            }
        }
    }
}