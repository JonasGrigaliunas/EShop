using EShop.Models.CategoryViewModels;
using EShop.Writers;

namespace EShop.Handlers.Category
{
    public class CreateCategoryHandler : ICreateCategoryHandler
    {
        private readonly IWriter<Data.Category> _categoryWriter;
        private readonly ITransactionProvider _transactionProvider;

        public CreateCategoryHandler(IWriter<Data.Category> categoryWriter, ITransactionProvider transactionProvider)
        {
            _categoryWriter = categoryWriter;
            _transactionProvider = transactionProvider;
        }

        public void Handle(CreateCategory createCategory)
        {
            var newCategory = new Data.Category
            {
                Name = createCategory.Name,
                Description = createCategory.Description,
                ParentCategoryId = createCategory.ParentCategoryId
   
            };

            _categoryWriter.Add(newCategory);
            _transactionProvider.Save();
        }
    }
}