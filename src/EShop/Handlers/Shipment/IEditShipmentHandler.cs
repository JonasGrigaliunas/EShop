using EShop.Areas.Warehouse.Models.ShipmentViewModel;


namespace EShop.Handlers.Shipment
{
    public interface IEditShipmentHandler
    {
       void Handle(EditShipment shipment);
    }
}
