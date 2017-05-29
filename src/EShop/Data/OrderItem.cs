using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Data
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
