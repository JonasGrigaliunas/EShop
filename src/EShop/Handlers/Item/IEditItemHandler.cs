using EShop.Areas.Sales.Models;
using EShop.Areas.Warehouse.Models;
using EShop.Models.ItemViewModel;

namespace EShop.Handlers.Item
{
    public interface IEditItemHandler
    {
        void Handle(EditItem editItem);
        void Handle(EditPrice editPrice);
        void Handle(EditQuantity editQuantity);
    }
}
