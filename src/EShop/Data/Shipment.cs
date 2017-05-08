using System.Collections.Generic;

namespace EShop.Data
{
    public class Shipment
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string From { get; set; }

        public IList<ShipmentItem> Items { get; set; }
    }
}
