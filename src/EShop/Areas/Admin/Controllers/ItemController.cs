using EShop.Data;
using EShop.Handlers.Item;
using EShop.Models.ItemViewModel;
using EShop.Readers;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public IActionResult Create(CreateItem item)
        {
            _createItemHandler.Handle(item);

            return RedirectToAction("Details", "Category", new
            {
                id = item.CategoryId
            });
        }
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Category");
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


            var itemDetails = new EditItem()
            {
                Id = chosenItem.Id,
                Name = chosenItem.Name,
                Description = chosenItem.Description,
                Price = chosenItem.Price,
                Quantity = chosenItem.Quantity,
                CategoryId = chosenItem.CategoryId,
                Code = chosenItem.Code
            };
            return View(itemDetails);
        }

        //POST: Item/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditItem item)
        {
            if (ModelState.IsValid)
            {
                _editItemHandler.Handle(item);

                return RedirectToAction("Details", new { id = item.Id });
            }
            return View(item);
        }
    }
}