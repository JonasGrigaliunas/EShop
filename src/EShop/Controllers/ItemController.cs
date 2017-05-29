using EShop.Data;
using EShop.Handlers.Item;
using EShop.Models;
using EShop.Models.ItemViewModel;
using EShop.Readers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EShop.Controllers
{
    public class ItemController : Controller
    {
        private readonly ICreateItemHandler _createItemHandler;
        private readonly IReader<Item> _itemReader;
        private readonly IEditItemHandler _editItemHandler;
        private readonly IBuyItemHandler _buyItemHandler;

        protected ApplicationDbContext ApplicationDbContext { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;


        public ItemController(ICreateItemHandler createItemHandler, IReader<Item> itemReader, IEditItemHandler editItemHandler, UserManager<ApplicationUser> userManager, IBuyItemHandler buyItemHandler)
        {
            _createItemHandler = createItemHandler;
            _itemReader = itemReader;
            _editItemHandler = editItemHandler;
            _userManager = userManager;
            _buyItemHandler = buyItemHandler;
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


            var itemDetails = new EditItem()
            {
                Id = chosenItem.Id,
                Name = chosenItem.Name,
                Code = chosenItem.Code,
                Description = chosenItem.Description,
                Price = chosenItem.Price,
                Quantity = chosenItem.Quantity,
                CategoryId = chosenItem.CategoryId
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

        // GET: Item/BuyItem
        public ActionResult BuyItem(int? id)
        {
            //var buyItem = _itemReader.Get(id);
            if (!id.HasValue)
            {
                RedirectToAction("Index", "Category");
            }
            var chosenItem = _itemReader.Get(id.Value);

            BuyItemViewModel itemDetails = new BuyItemViewModel()
            {
                Id = chosenItem.Id,
                Name = chosenItem.Name,
                Code = chosenItem.Code,
                Description = chosenItem.Description,
                Price = chosenItem.Price,
                Quantity = chosenItem.Quantity,
                CategoryId = chosenItem.CategoryId
            };

            return View(itemDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyItem(BuyItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                string UserId = _userManager.GetUserId(HttpContext.User);

                _buyItemHandler.CreateOrder(item, UserId);
                return RedirectToAction("Details", "Category", new { Id = item.CategoryId });
            }

            return View(item);
        }
        
    }
}