using System.Data;
using Microsoft.AspNetCore.JsonPatch.Internal;

public class SalesService
{
    private DataClient _connection;
    public SalesService(DataClient connection){
        _connection = connection;
    }

    public List<SalesModel>? GetAll()
    {
        try{
            var result = _connection.GetResultsFromQuery<SalesModel>(
                "SELECT * " +
                "FROM("+
                "SELECT soh.[SalesPersonID], "+
                "CONCAT(p.[FirstName], ' ', COALESCE(p.[MiddleName], ''), ' ', p.[LastName]) AS [FullName], "+
                "e.[JobTitle], st.[Name] AS [SalesTerritory], st.[Group], soh.[SubTotal], "+
                "YEAR(DATEADD(m, 6, soh.[OrderDate])) AS [FiscalYear] "+
                "FROM [Sales].[SalesPerson] sp "+
                "INNER JOIN [Sales].[SalesOrderHeader] soh ON sp.[BusinessEntityID] = soh.[SalesPersonID] "+
                "INNER JOIN [Sales].[SalesTerritory] st ON sp.[TerritoryID] = st.[TerritoryID] "+
                "INNER JOIN [HumanResources].[Employee] e ON soh.[SalesPersonID] = e.[BusinessEntityID] "+
                "INNER JOIN [Person].[Person] p ON p.[BusinessEntityID] = sp.[BusinessEntityID]"+
                ") AS soh "+
                "ORDER BY soh.[FiscalYear] DESC", Map);         
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<SalesModel>? GetSalesByNameAndYear(string name, string year){
        try{
            var result = _connection.GetResultsFromQuery<SalesModel>(
                "SELECT * " +
                "FROM( "+
                "SELECT soh.[SalesPersonID], "+
                "CONCAT(p.[FirstName], ' ', COALESCE(p.[MiddleName], ''), ' ', p.[LastName]) AS [FullName], "+
                "e.[JobTitle], st.[Name] AS [SalesTerritory], st.[Group], soh.[SubTotal], "+
                "YEAR(DATEADD(m, 6, soh.[OrderDate])) AS [FiscalYear] "+
                "FROM [Sales].[SalesPerson] sp "+
                "INNER JOIN [Sales].[SalesOrderHeader] soh ON sp.[BusinessEntityID] = soh.[SalesPersonID] "+
                "INNER JOIN [Sales].[SalesTerritory] st ON sp.[TerritoryID] = st.[TerritoryID] "+
                "INNER JOIN [HumanResources].[Employee] e ON soh.[SalesPersonID] = e.[BusinessEntityID] "+
                "INNER JOIN [Person].[Person] p ON p.[BusinessEntityID] = sp.[BusinessEntityID] "+
                ") AS soh "+
                $"WHERE soh.[FullName] LIKE '%{name}%' AND soh.[FiscalYear] LIKE '%{year}%' ORDER BY soh.[FiscalYear] DESC", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public SalesModel Map(IDataRecord record){
        SalesModel sales = new SalesModel();
            sales.SalesPersonID = (int)record["SalesPersonID"];
            sales.FullName = record["FullName"] as string;
            sales.JobTitle = record["JobTitle"] as string;
            sales.SalesTerritory = record["SalesTerritory"] as string;
            sales.Group = record["Group"] as string;
            sales.SubTotal = record["SubTotal"] as decimal?;
            sales.FiscalYear = (int)record["FiscalYear"];

        return sales;
            
    }
}