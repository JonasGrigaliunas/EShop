using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Warehouse.Models.ShipmentViewModel
{
    public class ShipmentItemViewModel
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
        public int Quantity { get; set; }

        public int ShipmentId { get; set; }

        //ViewModel
        public virtual Shipment Shipment { get; set; }

        public int ItemId { get; set; }

        //ViewModel
        public virtual Item Item { get; set; }
    }
}
