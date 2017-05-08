using EShop.Models.CategoryViewModels;

namespace EShop.Handlers.Category
{
    public interface ICreateCategoryHandler
    {
        void Handle(CreateCategory createCategory);
    }
}