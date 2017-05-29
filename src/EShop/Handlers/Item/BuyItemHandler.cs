using EShop.Models.ItemViewModel;
using EShop.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Handlers.Item
{
    public class BuyItemHandler : IBuyItemHandler
    {
        private readonly IWriter<Data.Order> _orderWriter;
        private readonly IWriter<Data.OrderItem> _orderItemWriter;
        private readonly ITransactionProvider _transactionProvider;

        public BuyItemHandler(IWriter<Data.OrderItem> orderItemWriter, ITransactionProvider transactionProvider, IWriter<Data.Order> orderWriter)
        {
            _orderWriter = orderWriter;
            _orderItemWriter = orderItemWriter;
            _transactionProvider = transactionProvider;
        }

        public void CreateOrder(BuyItemViewModel item, string userId)
        {
            var newOrder = new Data.Order
            {
                UserId = userId
                // Siame handleryje nepavyko iskviesti string UserId = _userManager.GetUserId(HttpContext.User);
                // Nerado HttpContext ir pasiulymai neveike
            };
            _orderWriter.Add(newOrder);

            var newOrderItem = new Data.OrderItem
            {
                ItemId = item.Id,
                Quantity = 1,
                Order = newOrder                
            };
            _orderItemWriter.Add(newOrderItem);

            // Writer iraso duomenis i DB?
            newOrder.OrderItems.Add(newOrderItem);
            _transactionProvider.Save();
        } 
    }
}
