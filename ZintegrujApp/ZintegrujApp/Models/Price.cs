using Dapper.Contrib.Extensions;

namespace ZintegrujApp.Models
{
    public class Price
    {
        [ExplicitKey]
        public string InternalID { get; set; }
        public string SKU { get; set; }
        public float NettPrice { get; set; }
        public float DiscountedPrice { get; set; }
        public int VAT { get; set; }
        public float DiscountedPriceLogisticUnit { get; set; }
    }
}
