using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models.CategoryViewModels
{
    public class CreateCategory
    {
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
