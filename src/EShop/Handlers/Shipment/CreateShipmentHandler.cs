using EShop.Handlers.Shipment;
using EShop.Data;
using EShop.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Areas.Warehouse.Models.ShipmentViewModel;
using EShop.Writers;

namespace EShop.Handlers.Shipment
{
    public class CreateShipmentHandler : ICreateShipmentHandler
    { 
    private readonly ITransactionProvider _transactionProvider;
    private readonly IWriter<Data.Shipment> _shipmentWriter;

    public CreateShipmentHandler(IWriter<Data.Shipment> shipmentWriter, ITransactionProvider transactionProvider)
    {
        _shipmentWriter = shipmentWriter;
        _transactionProvider = transactionProvider;
    }
    
        public void Handle(CreateShipment createShipment)
        {
            var newShipment = new Data.Shipment
            {
                Number = createShipment.Number,
                From = createShipment.From
            };

            _shipmentWriter.Add(newShipment);
            _transactionProvider.Save();
        }
    }
}
