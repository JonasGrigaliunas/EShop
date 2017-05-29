using EShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Data
{
    public class Order
    {
        public int Id { get; set; }
        public IList<OrderItem> OrderItems { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string E_Mail { get; set; }
        public string Tel_Number { get; set; }
        public string Address { get; set; }

        // Order should have DataTime property. Few items can be put in one Order, but
        // order is made in certain date

        // Property for User
        public ApplicationUser User { get; set; }
        public string UserId { set; get; }

        // reikes veliau apsirasyti readerius
    }
}
