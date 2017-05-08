using System;
using EShop.Areas.Sales.Models;
using EShop.Areas.Warehouse.Models;
using EShop.Data;
using EShop.Handlers.Item;
using EShop.Models.ItemViewModel;
using EShop.Readers;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ItemController : Controller
    {
        private readonly IReader<Item> _itemReader;
        private readonly IEditItemHandler _editItemHandler;

        // I should change constructor and add _itemReader to it
        public ItemController(IReader<Item> itemReader, IEditItemHandler editItemHandler)
        {
            _itemReader = itemReader;
            _editItemHandler = editItemHandler;
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                RedirectToAction("Index", "Category");
            }
            var chosenItem = _itemReader.Get(id.Value);
            

            ItemDetailsViewModel itemDetails = new ItemDetailsViewModel()
            {
                Id  = chosenItem.Id,
                Name = chosenItem.Name,
                Code = chosenItem.Code,
                Description = chosenItem.Description,
                Price = chosenItem.Price,
                Quantity = chosenItem.Quantity,
                CategoryId = chosenItem.CategoryId
            };

            return View(itemDetails);
        }

        // GET: Item/Edit
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                RedirectToAction("Index", "Category");
            }
            var chosenItem = _itemReader.Get(id.Value);


            var itemDetails = new EditQuantityViewModel()
            {
                EditQuantity = new EditQuantity
                {
                    Id = chosenItem.Id
                }
            };

            FillViewData(itemDetails, chosenItem);
            return View(itemDetails);
        }

     

        //POST: Item/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditQuantityViewModel item)
        {
            if (ModelState.IsValid)
            {
                _editItemHandler.Handle(item.EditQuantity);

                return RedirectToAction("Details", new { id = item.EditQuantity.Id });
            }

            var chosenItem = _itemReader.Get(item.EditQuantity.Id);
            FillViewData(item, chosenItem);

            return View(item);
        }

        private void FillViewData(EditQuantityViewModel viewModel, Item item)
        {
            viewModel.Name = item.Name;
            viewModel.Description = item.Description;
            viewModel.Price = item.Price;
            viewModel.CategoryId = item.CategoryId;
            viewModel.Code = item.Code;
            viewModel.Quantity = item.Quantity;
        }
    }
}