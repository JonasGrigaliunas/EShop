using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Warehouse.Models.ShipmentViewModel
{
    public class CreateShipment
    {
        public string Number { get; set; }
        public string From { get; set; }

        //Use viewModel
        public IList<ShipmentItem> Items { get; set; }
    }
}
