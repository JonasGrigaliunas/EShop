using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Warehouse.Models.ShipmentViewModel
{
    public class ItemListViewModel
    {

        public int Id { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }

        public int QuantityToShipment { get; set; }


    }
}
