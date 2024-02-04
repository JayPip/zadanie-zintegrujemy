using Dapper.Contrib.Extensions;

namespace ZintegrujApp.Models
{
    [Table("Inventory")]
    public class Inventory
    {
        [ExplicitKey]
        public int ProductID { get; set; }
        public string SKU { get; set; }
        public string Unit { get; set; }
        public int Qty { get; set; }
        public string Manufacturer { get; set; }
        public string Shipping { get; set; }
        public float ShippingCost { get; set; }
    }
}
