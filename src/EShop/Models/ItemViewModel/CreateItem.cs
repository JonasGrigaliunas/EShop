﻿namespace EShop.Models.ItemViewModel
{
    public class CreateItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public string Code { get; set; }
}
}