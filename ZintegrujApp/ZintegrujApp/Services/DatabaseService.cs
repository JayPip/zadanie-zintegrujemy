using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ZintegrujApp.Models;
using Dapper.Contrib.Extensions;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection");
    }

    //generic method for inserting list of objects into database
    public long InsertData<T>(List<T> data)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            //usage of dapper.contrib package
            return dbConnection.Insert(data);
        }
    }

    public List<InventoryProductInfo> GetProductInfoBySku(string sku)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            string query = "SELECT p.Name AS ProductName, " +
                "p.EAN, " +
                "p.ProducerName, " +
                "p.Category, " +
                "p.DefaultImage , " +
                "i.Qty , " +
                "i.Unit , " +
                "pr.DiscountedPriceLogisticUnit , " +
                "i.ShippingCost " +
                "FROM Products p " +
                "JOIN Inventory i ON p.SKU = i.SKU " +
                "JOIN Prices pr ON p.SKU = pr.SKU " +
                "WHERE p.SKU = @Sku";
            var result = dbConnection.Query<InventoryProductInfo>(query, new { Sku = sku });

            return result.AsList();
        }
    }
}