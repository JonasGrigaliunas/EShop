using System.Collections.Generic;
using EShop.Data;

namespace EShop.Models.CategoryViewModels
{
    public class CategoryItemDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<ChildCategory> ChildCategories { get; set; }
        public IList<CategoryItem> Items { get; set; }

        public IList<CategoryPathFragment> CategoryPath { set; get; }

        public class CategoryPathFragment
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class ChildCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class CategoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            
        }
    }
}