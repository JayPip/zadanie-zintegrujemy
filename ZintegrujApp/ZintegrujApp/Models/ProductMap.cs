using CsvHelper.Configuration;

namespace ZintegrujApp.Models
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Map(m => m.ID).Name("ID");
            Map(m => m.SKU).Name("SKU");
            Map(m => m.Name).Name("name");
            Map(m => m.EAN).Name("EAN");
            Map(m => m.ProducerName).Name("producer_name");
            Map(m => m.Category).Name("category").Default("");
            Map(m => m.IsWire).Name("is_wire").Default(0);
            Map(m => m.Available).Name("available_in_parcel_locker").Default(0);
            Map(m => m.IsVendor).Name("is_vendor").Default(0);
            Map(m => m.DefaultImage).Name("default_image").Default("");
        }
    }
}
