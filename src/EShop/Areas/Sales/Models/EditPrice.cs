namespace EShop.Areas.Sales.Models
{
    public class EditPriceViewModel
    {
        public EditPrice EditPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
    }

    public class EditPrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
