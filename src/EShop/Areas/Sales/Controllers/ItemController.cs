using EShop.Areas.Sales.Models;
using EShop.Data;
using EShop.Handlers.Item;
using EShop.Models.ItemViewModel;
using EShop.Readers;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class ItemController : Controller
    {
        private readonly ICreateItemHandler _createItemHandler;
        private readonly IReader<Item> _itemReader;
        private readonly IEditItemHandler _editItemHandler;

        public ItemController(ICreateItemHandler createItemHandler, IReader<Item> itemReader, IEditItemHandler editItemHandler)
        {
            _createItemHandler = createItemHandler;
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
                Description = chosenItem.Description,
                Price = chosenItem.Price,
                Quantity = chosenItem.Quantity,
                CategoryId = chosenItem.CategoryId,
                Code = chosenItem.Code               
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

            var itemDetails = new EditPriceViewModel()
            {
                EditPrice = new EditPrice
                {
                    Id = chosenItem.Id,
                    Price = chosenItem.Price
                }
            };

            FillViewData(itemDetails, chosenItem);
            return View(itemDetails);
        }

        //POST: Item/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPriceViewModel item)
        {
            if (ModelState.IsValid)
            {
                _editItemHandler.Handle(item.EditPrice);

                return RedirectToAction("Details", new { id = item.EditPrice.Id });
            }

            var chosenItem = _itemReader.Get(item.EditPrice.Id);
            FillViewData(item, chosenItem);

            return View(item);
        }

        private void FillViewData(EditPriceViewModel viewModel, Item item)
        {
            viewModel.Name = item.Name;
            viewModel.Description = item.Description;
            viewModel.Quantity = item.Quantity;
            viewModel.CategoryId = item.CategoryId;
            viewModel.Code = item.Code;
        }
    }
}