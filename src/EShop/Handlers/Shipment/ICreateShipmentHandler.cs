using EShop.Areas.Warehouse.Models.ShipmentViewModel;
using EShop.Handlers.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Handlers.Shipment
{
    public interface ICreateShipmentHandler
    {
        void Handle(CreateShipment createShipment);
    }
}
