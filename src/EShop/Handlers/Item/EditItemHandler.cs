using System;
using EShop.Areas.Sales.Models;
using EShop.Models.ItemViewModel;
using EShop.Readers;
using EShop.Areas.Warehouse.Models;

namespace EShop.Handlers.Item
{
    public class EditItemHandler : IEditItemHandler
    {
        private readonly IReader<Data.Item> _itemReader;
        private readonly ITransactionProvider _transactionProvider;

        public EditItemHandler (IReader<Data.Item> itemReader, ITransactionProvider transactionProvider)
        {
            _itemReader = itemReader;
            _transactionProvider = transactionProvider;
        }

        public void Handle (EditItem editItem)
        {
            var itemToDb = _itemReader.Get(editItem.Id);

            itemToDb.Name = editItem.Name;
            itemToDb.Description = editItem.Description;
            itemToDb.Price = editItem.Price;
            itemToDb.Quantity = editItem.Quantity;
            itemToDb.Code = editItem.Code;

            _transactionProvider.Save();
        }

        public void Handle(EditPrice editPrice)
        {
            var itemToDb = _itemReader.Get(editPrice.Id);

            itemToDb.Price = editPrice.Price;

            _transactionProvider.Save();
        }

        public void Handle(EditQuantity editQuantity)
        {
            var itemToDb = _itemReader.Get(editQuantity.Id);

            itemToDb.Quantity = itemToDb.Quantity + editQuantity.ChangeOfQuantity;

            _transactionProvider.Save();
        }
    }
}
