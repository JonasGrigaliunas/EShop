using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models.ItemViewModel
{
    public class BuyItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int QuantityInHouse { get; set; }
        public string Code { get; set; }

        public int CategoryId { get; set; }
    }
}
