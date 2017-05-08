using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Data;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace EShop.Readers
{
    public class Reader<TModel> : IReader<TModel>
        where TModel : class
    {
        private readonly ApplicationDbContext _dbContext;

        protected DbSet<TModel> DataSet
        {
            get { return _dbContext.Set<TModel>(); }
        }

        public Reader(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TModel> GetAll()
        {
            return DataSet.AsQueryable();
        }

        public virtual TModel Get(int id)
        {
            return DataSet.Find(id);
        }

        public Category Get(int? id)
        {
            // cia prirasiau exception, kad nemestu error, kai leidziu Detail controler su "int? id"
            throw new NotImplementedException();
        }
    }
}