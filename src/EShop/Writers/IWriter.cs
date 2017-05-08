namespace EShop.Writers
{
    public interface IWriter<TModel> where TModel : class
    {
        void Add(TModel entity);
        void Delete(TModel entity);
    }
}