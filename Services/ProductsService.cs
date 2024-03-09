using System.Data;
using Microsoft.AspNetCore.Mvc;

public class ProductsService : BaseProductsService
{
    private DataClient _connection;
    public ProductsService(DataClient connection)
    {
        _connection = connection;
    }
    public List<ProductModel>? GetAll()
    {
        try
        {
            var result = _connection.GetResultsFromQuery<ProductModel>("SELECT * FROM [Production].[Product]", Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductModel>? GetProductByCategoryType(string categoryType)
    {
        string sqlQuery = "SELECT " +
            $"P.* FROM Production.Product AS P " +
            $"INNER JOIN Production.ProductSubcategory AS PSC ON P.ProductSubcategoryID = PSC.ProductSubcategoryID " +
            $"INNER JOIN Production.ProductCategory AS PC ON PSC.ProductCategoryID = PC.ProductCategoryID " +
            $"WHERE PC.Name LIKE '%{categoryType}%'";

        try
        {
            var result = _connection.GetResultsFromQuery<ProductModel>(sqlQuery, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        
        return null; // Add this line to return a value in case of an exception.
    }

    public List<ProductModel>? GetProductByName(string name)
    {
        string sqlQuery = $"SELECT * FROM Production.Product WHERE Name LIKE '%{name}%'";
        try
        {
            var result = _connection.GetResultsFromQuery<ProductModel>(sqlQuery, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        
        return null; // Add this line to return a value in case of an exception.
    }

    public ProductModel Map(IDataRecord record)
    {
        ProductModel product = new ProductModel();
        product.ProductID = (int)record["ProductID"];
        product.Name = record["Name"] as string;
        product.ProductNumber = record["ProductNumber"] as string;
        product.MakeFlag = (bool)record["MakeFlag"];
        product.FinishedGoodsFlag = (bool)record["FinishedGoodsFlag"];
        product.Color = record["Color"] as string ?? "";
        product.SafetyStockLevel = (short)record["SafetyStockLevel"];
        product.ReorderPoint = (short)record["ReorderPoint"];
        product.StandardCost = (decimal)record["StandardCost"];
        product.ListPrice = (decimal)record["ListPrice"];
        return product;
    }

}