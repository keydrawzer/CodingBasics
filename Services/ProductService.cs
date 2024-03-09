using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;

public class ProductService
{
    private readonly DataClient _connection;

    public ProductService(DataClient connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public List<ProductsModel>? GetAll()
    {
        try
        {
            string query = "SELECT * FROM [AdventureWorks2019].[Production].[Product];";
            var result = _connection.GetResultsFromQuery<ProductsModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving all products: {ex.Message}");
        }
        return null;
    }

    public List<ProductsModel>? GetProductsByName(string name)
    {
        try
        {
            string query = $"SELECT * FROM [AdventureWorks2019].[Production].[Product] WHERE CONCAT(Name, ' ') LIKE '%{name}%';";
            var result = _connection.GetResultsFromQuery<ProductsModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving products by name: {ex.Message}");
        }
        return null;
    }

    public List<ProductsModel>? GetProductsByCategory(string categoryType)
    {
        try
        {
            string query = $"SELECT * FROM [AdventureWorks2019].[Production].[Product] AS P " +
                           "INNER JOIN [Production].[ProductSubcategory] AS PS ON P.ProductSubcategoryID = PS.ProductSubcategoryID " +
                           "INNER JOIN [Production].[ProductCategory] AS PC ON PS.ProductCategoryID = PC.ProductCategoryID " +
                           $"WHERE PC.Name LIKE '%{categoryType}%';";
            var result = _connection.GetResultsFromQuery<ProductsModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving products by category: {ex.Message}");
        }
        return null;
    }

    public ProductsModel Map(IDataRecord record)
    {
        return new ProductsModel
        {
            ProductID = (int)record["ProductID"],
            Name = record["Name"] as string,
            ProductNumber = record["ProductNumber"] as string,
            Color = record["Color"] as string,
            StandardCost = (decimal)record["StandardCost"],
            ListPrice = (decimal)record["ListPrice"],
            Size = record["Size"] as string,
            Weight = record["Weight"] != DBNull.Value ? (decimal?)record["Weight"] : null,
            Class = record["Class"] as string,
            Style = record["Style"] as string
        };
    }
}
