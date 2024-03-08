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
                "SELECT prod.ProductID, prod.Name, prod.ProductNumber " +
                "FROM Production.Product prod " +
                "INNER JOIN Production.ProductSubcategory subcat ON prod.ProductSubcategoryID = subcat.ProductSubcategoryID "+
                "INNER JOIN Production.ProductCategory cat ON subcat.ProductCategoryID = cat.ProductCategoryID "+ 
                 $"WHERE cat.Name LIKE '%{categoryType}%'", Map);
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
            return product;   
    }
    
}