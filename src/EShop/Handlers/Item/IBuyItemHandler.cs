using EShop.Models.ItemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Handlers.Item
{
    public interface IBuyItemHandler
    {
        void CreateOrder(BuyItemViewModel item, string userId);
    }
}
