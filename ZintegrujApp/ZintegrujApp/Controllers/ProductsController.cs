using Microsoft.AspNetCore.Mvc;
using ZintegrujApp.Models;
using ZintegrujApp.Services;

namespace ZintegrujApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly CsvFileService _csvFileService;
        private readonly DatabaseService _databaseService;

        public ProductsController(CsvFileService csvFileManager, DatabaseService databaseManager)
        {
            _csvFileService = csvFileManager;
            _databaseService = databaseManager;
        }

        [HttpGet("import")]
        public IActionResult ImportData()
        {
            try
            {
                // Download files from Urls
                _csvFileService.DownloadFile("https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv", "Products.csv");
                _csvFileService.DownloadFile("https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv", "Inventory.csv");
                _csvFileService.DownloadFile("https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv", "Prices.csv");

                //Read data from CSV files
                var productsList = _csvFileService.ReadProductsFromCsv("Products.csv");
                var inventoryList = _csvFileService.ReadInventoryFromCsv("Inventory.csv");
                var pricesList = _csvFileService.ReadPricesFromCsv("Prices.csv");

                // Create a HashSet for faster lookups
                var inventorySKUSet = new HashSet<string>(inventoryList.Select(inventory => inventory.SKU));

                // LINQ query to filter Product list based on conditions
                var filteredProducts = productsList
                    .Where(product => inventorySKUSet.Contains(product.SKU))
                    .ToList();

                //Insert data to database tables
                _databaseService.InsertData(filteredProducts);

                _databaseService.InsertData(inventoryList);

                _databaseService.InsertData(pricesList);

                return Ok("Data imported successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error importing data: {ex.Message}");
            }
        }

        [HttpGet("{sku}")]
        public IActionResult GetProductBySku(string sku)
        {
            try
            {
                var productInfo = _databaseService.GetProductInfoBySku(sku);

                if (productInfo == null)
                    return NotFound("Product not found");

                return Ok(productInfo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving product information: {ex.Message}");
            }
        }
    }
}
