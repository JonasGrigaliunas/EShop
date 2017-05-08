using EShop.Areas.Warehouse.Models.ShipmentViewModel;
using EShop.Data;
using EShop.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ViewModelReaders
{
    public class ItemListReader
    {
        private readonly IReader<Item> _itemReader;

        public ItemListReader(IReader<Item> itemReader)
        {
            _itemReader = itemReader;
        }

        public IList<ItemListViewModel> Read()
        {
            return _itemReader
                .GetAll()
                .Select(x => new ItemListViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Checked = false,
                    QuantityToShipment = 1
                }).ToList();
        }
    }
}
