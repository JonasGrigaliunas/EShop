using EShop.Areas.Warehouse.Models.ShipmentViewModel;
using EShop.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Handlers.Shipment
{
    public class EditShipmentHandler : IEditShipmentHandler
    {
        private readonly IReader<Data.Shipment> _shipmentReader;
        private readonly ITransactionProvider _transactionProvider;

        public EditShipmentHandler(IReader<Data.Shipment> shipmentReader, ITransactionProvider transactionProvider)
        {
            _shipmentReader = shipmentReader;
            _transactionProvider = transactionProvider;
        }

        public void Handle( EditShipment shipment)
        {
            var shipmentToDb = _shipmentReader.Get(shipment.Id);

            shipmentToDb.From = shipment.From;
            shipmentToDb.Number = shipment.Number;

            _transactionProvider.Save();

        }


    }
}
