namespace EShop.Areas.Warehouse.Models
{
    public class EditQuantityViewModel
    {
        public EditQuantity EditQuantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Code { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }

    public class EditQuantity
    {
        public int Id { get; set; }
        public int ChangeOfQuantity { get; set; }
    }
}
