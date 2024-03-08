using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SaleService
{
    private DataClient _connection;
    public SaleService(DataClient connection){
        _connection = connection;
    }
    public List<SalesModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<SalesModel>("SELECT * FROM [Sales].[vSalesReview]", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<SalesModel>? GetBynameAndYear(string name, string yearSale){
        try{
            var result = _connection.GetResultsFromQuery<SalesModel>(
                "SELECT * " +
                "FROM [AdventureWorks2022].[Sales].[vSalesReview] " +
                $"WHERE [Sales].[vSalesReview].[FullName] LIKE '%{name}%' AND [Sales].[vSalesReview].[SelectDate] LIKE '%{yearSale}%' AND [Sales].[vSalesReview].[SelectDate] is NOT null", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public SalesModel Map(IDataRecord record){
        SalesModel sale = new SalesModel();
        
            sale.SalesPersonID = (int)record["SalesPersonID"];
            sale.FullName = record["FullName"] as string;
            sale.JobTitle = record["JobTitle"] as string;
            sale.SalesTerritory = record["SalesTerritory"] as string;
            sale.TerritoryName = record["TerritoryName"] as string;
            sale.TerritoryGroup = record["TerritoryGroup"] as string;
            sale.SelectDate = record["SelectDate"] as string;
            sale.SalesQuota = record["SalesQuota"] as decimal?;
            sale.SalesYTD = record["SalesYTD"] as decimal?;
            sale.SalesLastYear = record["SalesLastYear"] as decimal?;
            
            return sale;
    }
}