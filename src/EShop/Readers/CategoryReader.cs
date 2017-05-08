using System.Linq;
using EShop.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Readers
{
    public class CategoryReader : Reader<Category>
    {
        public CategoryReader(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override Category Get(int id)
        {
            return DataSet
                .Include(category => category.ChildCategories)
                .Include(category => category.ParentCategory)
                .Include(category => category.Items)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}