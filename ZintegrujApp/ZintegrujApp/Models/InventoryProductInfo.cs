namespace ZintegrujApp.Models
{
    public class InventoryProductInfo
    {
        public string ProductName { get; set; }
        public string EAN { get; set; }
        public string ProducerName { get; set; }
        public string Category { get; set; }
        public string DefaultImage { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public string DiscountedPriceLogisticUnit { get; set; }
        public float ShippingCost { get; set; }
    }
}
