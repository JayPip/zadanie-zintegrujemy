namespace ZintegrujApp.Services
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.SqlServer.Server;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net.NetworkInformation;
    using ZintegrujApp.Models;

    public class CsvFileService
    {
        public List<Product> ReadProductsFromCsv(string filePath)
        {
            //config for specific csv file
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                Delimiter = ";",
                //filtering csv file using should skip record for performance
                ShouldSkipRecord = (args) => args.Row.Parser.Record.Length < 10 || args.Row[8].StartsWith("1")
        };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                //declaring usage of map
                csv.Context.RegisterClassMap<ProductMap>();
                var records = csv.GetRecords<Product>().ToList();
                return records;
            }
        }

        public List<Inventory> ReadInventoryFromCsv(string filePath)
        {
            //config for specific csv file
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                //filtering csv file using should skip record for performance
                ShouldSkipRecord = (args) => !args.Row[6].StartsWith("Wysyłka w 24h") && !args.Row[6].StartsWith("shipping")
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                //declaring usage of map
                csv.Context.RegisterClassMap<InventoryMap>();
                var records = csv.GetRecords<Inventory>().ToList();
                return records;
            }
        }

        public List<Price> ReadPricesFromCsv(string filePath)
        {
            //config for specific csv file
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                HasHeaderRecord = false
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                //declaring usage of map
                csv.Context.RegisterClassMap<PriceMap>();
                var records = csv.GetRecords<Price>().ToList();
                return records;
            }
        }

        public void DownloadFile(string fileUrl, string fileName)
        {
            //downloading csv file
            using (var client = new System.Net.WebClient())
            {
                client.DownloadFile(fileUrl, fileName);
            }
        }
    }
}
