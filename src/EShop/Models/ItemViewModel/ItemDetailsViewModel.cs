namespace EShop.Models.ItemViewModel
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }

        public int CategoryId { get; set; }
    }
}
