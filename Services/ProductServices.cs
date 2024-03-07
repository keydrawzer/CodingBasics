
using System.Data;
using Microsoft.AspNetCore.Mvc;

public class ProductService
{
    private DataClient _connection;
    public ProductService(DataClient connection)
    {
        _connection = connection;
    }
    public List<ProductsModel>? GetAll()
    {
        try
        {
            var result = _connection.GetResultsFromQuery<ProductsModel>("SELECT * FROM [Production].[vProductAndDescription]", Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductsModel>? GetProductsByName(string name)
    {
        try
        {
            var result = _connection.GetResultsFromQuery<ProductsModel>(
"SELECT * " +
"FROM [AdventureWorks2022].[Production].[vProductAndDescription] " +
$"WHERE Name LIKE '%{name}%'", Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductsModel>? GetProductByCategoryType(string categoryType)
    {
        try
        {
            var result = _connection.GetResultsFromQuery<ProductsModel>(
                "SELECT " +
                "a.ProductID, a.Name, d.Name as ProductModel, e.CultureID , f.Description " +
                "FROM [AdventureWorks2022].[Production].[Product] a " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductSubcategory]  b ON a.ProductSubcategoryID = b.ProductSubcategoryID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductCategory] c ON b.ProductCategoryID = c.ProductCategoryID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductModel] d ON a.ProductModelID = d.ProductModelID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductModelProductDescriptionCulture] e ON d.ProductModelID = e.ProductModelID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductDescription] f ON f.ProductDescriptionID = e.ProductDescriptionID " +
                $"WHERE c.Name LIKE '%{categoryType}%'", Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
        }
        return null;
    }

    public List<ProductsModel>? GetProductByNameAndCategoryType(string name, string categoryType)
    {
        try
        {
            var result = _connection.GetResultsFromQuery<ProductsModel>(
                "SELECT " +
                "a.ProductID, a.Name, d.Name as ProductModel, e.CultureID , f.Description " +
                "FROM [AdventureWorks2022].[Production].[Product] a " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductSubcategory]  b ON a.ProductSubcategoryID = b.ProductSubcategoryID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductCategory] c ON b.ProductCategoryID = c.ProductCategoryID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductModel] d ON a.ProductModelID = d.ProductModelID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductModelProductDescriptionCulture] e ON d.ProductModelID = e.ProductModelID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductDescription] f ON f.ProductDescriptionID = e.ProductDescriptionID " +
                $"WHERE a.Name LIKE '%{name}%' AND c.Name LIKE '%{categoryType}%' AND e.CultureID ='en'", Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }
    public ProductsModel Map(IDataRecord record)
    {
        ProductsModel product = new ProductsModel();
        product.ProductID = (int)record["ProductID"];
        product.Name = record["Name"] as string;
        product.ProductModel = record["ProductModel"] as string;
        product.CultureID = record["CultureID"] as string;
        product.Description = record["Description"] as string;
        return product;
    }
}
