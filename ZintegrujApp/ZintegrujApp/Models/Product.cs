using Dapper.Contrib.Extensions;

namespace ZintegrujApp.Models
{
    public class Product
    {
        [ExplicitKey]
        public int ID { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string EAN { get; set; }
        public string ProducerName { get; set; }
        public string Category { get; set; }
        public int IsWire { get; set; }
        public int Available { get; set; }
        public int IsVendor { get; set; }
        public string DefaultImage { get; set; }
    }
}
