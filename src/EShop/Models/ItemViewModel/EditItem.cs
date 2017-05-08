using System.ComponentModel.DataAnnotations;

namespace EShop.Models.ItemViewModel
{
    public class EditItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }

        public int CategoryId { get; set; }
    }
}
