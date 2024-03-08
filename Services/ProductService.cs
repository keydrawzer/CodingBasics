using System.Data;
using Microsoft.AspNetCore.Mvc;

public class ProductService
{
    private DataClient _connection;
    public ProductService(DataClient connection){
        _connection = connection;
    }

    public List<ProductModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>("SELECT ProductID, Name, ProductNumber FROM [Production].[Product]", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }
    
    public List<ProductModel>? GetProductByName(string name){
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>(
                "SELECT ProductID, Name, ProductNumber " +
                "FROM [Production].[Product] " +
                $"WHERE Name LIKE '%{name}%'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductModel>? GetProductByCategoryType(string categoryType){
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>(
                "SELECT a.ProductID, a.Name, a.ProductNumber " +
                "FROM [Production].[Product] a " +
                "INNER JOIN [Production].[ProductSubcategory]  b ON a.ProductSubcategoryID = b.ProductSubcategoryID " +
                "INNER JOIN [Production].[ProductCategory] c ON b.ProductCategoryID = c.ProductCategoryID " +
                $"WHERE c.Name LIKE '%{categoryType}%'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"Error message: {ex.Message}");
        }
        return null;
    }

     public ProductModel Map(IDataRecord record){
        ProductModel product = new ProductModel();
            product.ProductID = (int)record["ProductID"];
            product.Name = record["Name"] as string;
            product.ProductNumber = record["ProductNumber"] as string; 
            product.ProductSubcategoryID = (int)record["ProductSubcategoryID"];
            product.ProductCategoryID = (int)record["ProductCategoryID"];
            //product.NameCategory = record["Name"] as string;
            return product;   
    }
    
}