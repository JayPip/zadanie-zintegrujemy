using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ZintegrujApp.Models
{
    public class PriceMap : ClassMap<Price>
    {
        public PriceMap()
        {
            Map(m => m.InternalID).Index(0);
            Map(m => m.SKU).Index(1);
            Map(m => m.NettPrice).Index(2);
            Map(m => m.DiscountedPrice).Index(3);
            Map(m => m.VAT).Index(4).TypeConverter<CorrectIntConverter>();
            Map(m => m.DiscountedPriceLogisticUnit).Index(5).Default(0);
        }
    }

    public class CorrectIntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // Handle specific cases for invalid strings and convert them accordingly
            if (text.Equals("O", StringComparison.OrdinalIgnoreCase))
            {
                return 0; 
            }

            if (int.TryParse(text, out int result))
            {
                return result;
            }

            // Handle the case where the conversion fails, e.g., log an error or throw an exception
            throw new InvalidOperationException($"Failed to convert {memberMapData.Member.Name} to int: {text}");
        }
    }
}
