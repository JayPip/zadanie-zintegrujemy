using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Text.Json.Serialization;

namespace ZintegrujApp.Models
{
    public class InventoryMap : ClassMap<Inventory>
    {
        public InventoryMap()
        {
            Map(m => m.ProductID).Name("product_id");
            Map(m => m.SKU).Name("sku");
            Map(m => m.Unit).Name("unit");
            Map(m => m.Qty).Name("qty").TypeConverter<IntTypeConverter>();
            Map(m => m.Manufacturer).Name("manufacturer_name");
            Map(m => m.Shipping).Name("shipping");
            Map(m => m.ShippingCost).Name("shipping_cost").Default(0);
        }
    }

    public class IntTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return int.Parse(text.Split('.')[0]);

            // Handle the case where the conversion fails, e.g., log an error or throw an exception
            throw new InvalidOperationException($"Failed to convert {memberMapData.Member.Name} to int: {text}");
        }
    }
}
