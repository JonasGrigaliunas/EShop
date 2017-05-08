using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Readers
{
    public interface IItemReader : IReader<Item>
    {
        IQueryable<Item> ByCode(string searchBoxInput);
    }
}
