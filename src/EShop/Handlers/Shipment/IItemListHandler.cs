using EShop.Areas.Warehouse.Models.ShipmentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;

namespace EShop.Handlers.Shipment
{
    public interface IItemListHandler
    {
        List<ItemListViewModel> ToSelectableList(IList<Data.Item> allItems);
        void AddItems(EditShipment selectedItems, int id);

        List<ShipmentItemViewModel> ToSelectableList(IList<ShipmentItem> items);
        void RemoveItems(EditShipment selectedItems, int id);
    }
}
