using EShop.Models.ItemViewModel;

namespace EShop.Handlers.Item
{
    public interface ICreateItemHandler
    {
        void Handle(CreateItem createItem);
    }
}