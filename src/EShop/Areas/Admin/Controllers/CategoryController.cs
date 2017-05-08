using System.Collections.Generic;
using System.Linq;
using EShop.Data;
using EShop.Handlers.Category;
using EShop.Models.CategoryViewModels;
using EShop.Readers;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // readonly is field that is immutable (constant)
        private readonly IReader<Category> _categoryReader;
        private readonly ICreateCategoryHandler _createCategoryHandler;

        public CategoryController(IReader<Category> categoryReader, ICreateCategoryHandler createCategoryHandler)
        {
            _categoryReader = categoryReader;
            _createCategoryHandler = createCategoryHandler;
        }

        public IActionResult Index()
        {
            var categoryList = _categoryReader
                .GetAll()
                .Where(x => x.ParentCategoryId == null)
                .OrderBy(x => x.Name)
                .ToList();
            
            return View(categoryList);
        }

        public IActionResult Create(CreateCategory category)
        {
            _createCategoryHandler.Handle(category);
            
            if (category.ParentCategoryId.HasValue)
                return RedirectToAction("Details", new
                {
                    id = category.ParentCategoryId.Value
                });
            return RedirectToAction("Index");
        }

        // vietoj int turetu buti int?   int? reiskia, kad reiksme lygi intúi arba null
        public IActionResult Details(int? id)
        {

            if (!id.HasValue)
                return RedirectToAction("Index");

            Category chosenCategory = _categoryReader.Get(id.Value);

            CategoryItemDetailsViewModel categoryItemDetails = new CategoryItemDetailsViewModel()
            {
                Id = chosenCategory.Id,
                Name = chosenCategory.Name,
                Description = chosenCategory.Description,
                
                CategoryPath = GetPath(chosenCategory)
                .Reverse()
                .ToList()
            };

            categoryItemDetails.ChildCategories = chosenCategory.ChildCategories
                    .Select(x => new CategoryItemDetailsViewModel.ChildCategory()
                    {
                        Description = x.Description,
                        Name = x.Name,
                        Id = x.Id
                    }).OrderBy(x => x.Name)
                    .ToList();

            categoryItemDetails.Items = chosenCategory.Items
                .Select(x => new CategoryItemDetailsViewModel.CategoryItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Code = x.Code
                }).OrderBy(x => x.Name)
                .ToList();

            return View(categoryItemDetails);

        }

        private IEnumerable<CategoryItemDetailsViewModel.CategoryPathFragment> GetPath(Category currentCategory)
        {
            while (currentCategory.ParentCategoryId.HasValue)
            {
                var parentCategory = _categoryReader.Get(currentCategory.ParentCategoryId.Value);
                yield return new CategoryItemDetailsViewModel.CategoryPathFragment
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name
                };
                currentCategory = parentCategory;
            }
        }
    }
}