using EShop.Data;

namespace EShop.Writers
{
    public class Writer<TModel> : IWriter<TModel>
        where TModel : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Writer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TModel entity)
        {
            _dbContext.Set<TModel>().Add(entity);
        }

        public void Delete(TModel entity)
        {
            _dbContext.Set<TModel>().Remove(entity);
        }
    }
}