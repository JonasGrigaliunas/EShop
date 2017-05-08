using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.Readers
{
    public class ItemReader : Reader<Item>, IItemReader
    {
        public ItemReader(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override Item Get(int id)
        {
            return DataSet
                // Include reikia neprimityvioms klases uzkrauti kartu. Kokia kategorija uzkrauti apsirasiau ApDbContext "HasForeignKey".
                .Include(x => x.Category)
                // FirstOrDefault iesko pirmo iraso su tokiu Id ir ji grazina arba grazina null.
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Item> ByCode(string searchBoxInput)
        {
            if (!string.IsNullOrEmpty(searchBoxInput))
            {
                return GetAll().Where(x => x.Code.Contains(searchBoxInput));
            }
            else
            {
                return GetAll();
            }
        }
    }
}
