using EShop.Models.ItemViewModel;
using EShop.Readers;
using EShop.Writers;

namespace EShop.Handlers.Item
{
    public class CreateItemHandler : ICreateItemHandler
    {
        private readonly IReader<Data.Category> _categoryReader;
        private readonly IWriter<Data.Item> _itemWriter;
        private readonly ITransactionProvider _transactionProvider;

        public CreateItemHandler(IReader<Data.Category> categoryReader, IWriter<Data.Item> itemWriter,
            ITransactionProvider transactionProvider)
        {
            _categoryReader = categoryReader;
            _itemWriter = itemWriter;
            _transactionProvider = transactionProvider;
        }

        //This Handle creates item and adds it to specific category
        public void Handle(CreateItem createItem)
        {
            var category = _categoryReader.Get(createItem.CategoryId);

            var newItem = new Data.Item
            {
                Name = createItem.Name,
                Description = createItem.Description,
                Price = createItem.Price,
                Quantity = createItem.Quantity,
                CategoryId = category.Id,
                Code = createItem.Code
            };
            category.Items.Add(newItem);

            _itemWriter.Add(newItem);
            _transactionProvider.Save();
        }
    }
}