using System.Collections.Generic;

namespace EShop.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual IList<Category> ChildCategories { get; set; }

        public virtual IList<Item> Items { get; set; }
    }
}
