using System.Data;
using Microsoft.AspNetCore.Mvc;

public class ProductsService 
{
        private DataClient _connection;
    public ProductsService(DataClient connection){
        _connection = connection;
    }

        public List<ProductsModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<ProductsModel>("SELECT * FROM [AdventureWorks2022].[Production].[Product];", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<ProductsModel>? GetProductsByName(string name){
        try{
            var result = _connection.GetResultsFromQuery<ProductsModel>(
                "SELECT * " +
                "FROM [AdventureWorks2022].[Production].[Product]" +
                $"WHERE CONCAT(Name,' ') LIKE '%{name}%'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

        public List<ProductsModel>? GetProductsByCategory(string categoryType){
        try{
            var result = _connection.GetResultsFromQuery<ProductsModel>(
                 "SELECT * " +
            "FROM [AdventureWorks2022].[Production].[Product] AS P " +
            "INNER JOIN [Production].[ProductSubcategory] AS PS ON P.ProductSubcategoryID = PS.ProductSubcategoryID " +
            "INNER JOIN [Production].[ProductCategory] AS PC ON PS.ProductCategoryID = PC.ProductCategoryID " +
            $"WHERE PC.Name LIKE '%{categoryType}%'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

public ProductsModel Map(IDataRecord record)
{
    ProductsModel products = new ProductsModel();
    products.ProductID = (int)record["ProductID"];
    products.Name = record["Name"] as string;
    products.ProductNumber = record["ProductNumber"] as string;
    products.Color = record["Color"] as string;
    products.StandardCost = (decimal)record["StandardCost"];
    products.ListPrice = (decimal)record["ListPrice"];
    products.Size = record["Size"] as string;

     if (record["Weight"] != DBNull.Value)
     {
        products.Weight = (decimal)record["Weight"];
     }
     else
     {
         products.Weight = null;
     }

    products.Class = record["Class"] as string;
    products.Style = record["Style"] as string;

    return products;
}
}