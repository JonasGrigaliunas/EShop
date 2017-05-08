using System.Collections.Generic;

namespace EShop.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public IList<ShipmentItem> ShipmentItems { get; set; }
    }
}
