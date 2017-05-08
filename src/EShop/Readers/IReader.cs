using System.Collections.Generic;
using System.Linq;
using EShop.Data;

namespace EShop.Readers
{
    public interface IReader<TModel> 
        where TModel : class
    {
        TModel Get(int id);
        // IList is used when you get a list from database and then query (filter) in client
        // IQueryable is used when data is filtered in database first and then you get a List
        IQueryable<TModel> GetAll();

    }
}