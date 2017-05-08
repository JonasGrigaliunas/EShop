namespace EShop.Data
{
    public class ShipmentItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
