using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EShop.Areas.Warehouse.Models.ShipmentViewModel
{
    public class EditShipment
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string From { get; set; }

        public IList<ShipmentItemViewModel> Items { get; set; }

 
        public IList<ItemListViewModel> AllItems { get; set; }

    }
}
