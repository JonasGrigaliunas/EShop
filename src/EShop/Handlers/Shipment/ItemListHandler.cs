using EShop.Areas.Warehouse.Models.ShipmentViewModel;
using EShop.Data;
using EShop.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Handlers.Shipment
{
    public class ItemListHandler : IItemListHandler
    {
        // sio handlerio mintis turejo buti - paimti bet koki IList<Item> ir su nauju modeliu
        // prideti jam papildoma lauka (bool - checkbox) (atvaizdavimui), kuris igalintu prideti Item prie Shipment'o
        // 2 metodas tada Db pagal Id randa, kuriuos Item'us reikia prideti.

        private readonly IReader<Data.Item> _itemReader;
        private readonly ITransactionProvider _transactionProvider;
        private readonly IReader<Data.Shipment> _shipmentReader;
        private readonly IReader<ShipmentItem> _shipmentItemReader;

        public ItemListHandler(IReader<Data.Item> itemReader, ITransactionProvider transactionProvider, IReader<Data.Shipment> shipmentReader, IReader<ShipmentItem> shipmentItemReader)
        {
            _itemReader = itemReader;
            _transactionProvider = transactionProvider;
            _shipmentReader = shipmentReader;
            _shipmentItemReader = shipmentItemReader;
        }

        //Move to reader
        public List<ItemListViewModel> ToSelectableList(IList<Data.Item> allItems)
        {
            return allItems
                .Select(x => new ItemListViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Checked = false,
                    QuantityToShipment = 0
                }).ToList();
        }

        public void AddItems(EditShipment selectedItems, int id)
        {
            var editShipment = _shipmentReader.Get(id);

            var newShipmentitems = selectedItems.AllItems
                .Where(x => x.QuantityToShipment >= 1)
                // Select automatiskai pereina per visa IEnumerable? Selectui grazinamas new ShipmentItem ir tada jis ima kita nari?
                .Select(x =>
                {
                    var originalItem = _itemReader.Get(x.Id);

                    return new ShipmentItem()
                    {
                        Quantity = x.QuantityToShipment,
                        ShipmentId = id,
                        Shipment = editShipment,
                        ItemId = x.Id,
                        Item = originalItem
                    };
                });

            // Kodel ir Items sarasa prisideda nauji nariai, jei pries newShipmentItems nera "return"?
            newShipmentitems.
                ToList().
                ForEach(x=> {
                    editShipment.Items.Add(x);
                    x.Item.Quantity -= x.Quantity;
                });

            _transactionProvider.Save();
        }

        //Move to reader
        public List<ShipmentItemViewModel> ToSelectableList(IList<ShipmentItem> items)
        {
            List<ShipmentItemViewModel> newList = new List<ShipmentItemViewModel>();
            foreach (var shipmentItem in items)
            {
                ShipmentItemViewModel selectableShipmentItem = new ShipmentItemViewModel()
                {
                    Id = shipmentItem.Id,
                    Quantity = shipmentItem.Quantity,
                    ShipmentId = shipmentItem.ShipmentId,
                    Shipment = shipmentItem.Shipment,
                    ItemId = shipmentItem.ItemId,
                    Item = shipmentItem.Item,
                    Checked = false
                };
                newList.Add(selectableShipmentItem);
            }
            return newList;
                
        }

        //rewrite with where and ForEach
        public void RemoveItems(EditShipment selectedItems, int id)
        {
            var editShipment = _shipmentReader.Get(id);
            var removeShipmentItems = selectedItems.Items.Where(x => x.Checked == true);    

            foreach (var _checkedShipmentItem in removeShipmentItems)
            {
                var shipmentItem = _shipmentItemReader.Get(_checkedShipmentItem.Id);
                var item = _itemReader.Get(shipmentItem.ItemId);
                item.Quantity = item.Quantity + shipmentItem.Quantity;

                editShipment.Items.Remove(shipmentItem);
                
            }
            _transactionProvider.Save();
        }
    }
}
