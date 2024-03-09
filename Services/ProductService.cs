
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
            var result = _connection.GetResultsFromQuery<ProductModel>("SELECT * FROM [Production].[vProductAndDescription]", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductModel>? GetProductByName(string name){
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>(
                "SELECT * " +
                "FROM [AdventureWorks2022].[Production].[vProductAndDescription] " +
                $"WHERE Name LIKE '%{name}%'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductModel>? GetProductByModel(string model){
        try{
            var result = _connection.GetResultsFromQuery<ProductModel>(
                "SELECT * " +
                "FROM [AdventureWorks2022].[Production].[vProductAndDescription] " +
                $"WHERE ProductModel LIKE '%{model}%'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public ProductModel Map(IDataRecord record){
        ProductModel product = new ProductModel();
            product.ProductID = (int)record["ProductID"];
            product.Name = record["Name"] as string;
            product.Model = record["ProductModel"] as string;
            product.CultureID = record["CultureID"] as string;
            product.Description = record["Description"] as string;
        return product;

    }
}