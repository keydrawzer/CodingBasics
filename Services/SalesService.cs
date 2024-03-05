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
                "SELECT " +
                " pvt.[SalesPersonID], pvt.[FullName], pvt.[JobTitle], pvt.[SalesTerritory], pvt.[Group], pvt.[SalesQuota]," + 
                $" pvt.[SalesYTD], pvt.[SalesLastYear], pvt.[2011] " +
                "FROM (SELECT " + 
                "soh.[SalesPersonID], p.[FirstName] + ' ' + COALESCE(p.[MiddleName], '') + ' ' + p.[LastName] AS [FullName]," +
                " e.[JobTitle], st.[Name] AS [SalesTerritory], st.[Group], sp.[SalesQuota], sp.[SalesYTD], sp.[SalesLastYear]," +
                " soh.[SubTotal], YEAR(DATEADD(m, 6, soh.[OrderDate])) AS [FiscalYear] " +
                "FROM [Sales].[SalesPerson] sp " +
                "INNER JOIN [Sales].[SalesOrderHeader] soh " + 
                "ON sp.[BusinessEntityID] = soh.[SalesPersonID] " +
                "INNER JOIN [Sales].[SalesTerritory] st " +
                "ON sp.[TerritoryID] = st.[TerritoryID] " +
                "INNER JOIN [HumanResources].[Employee] e " +
                "ON soh.[SalesPersonID] = e.[BusinessEntityID] " +
                "INNER JOIN [Person].[Person] p " +
                "ON p.[BusinessEntityID] = sp.[BusinessEntityID] "+
                ") AS soh " +
                $"PIVOT ( SUM([SubTotal]) FOR [FiscalYear] IN ([2011]) ) AS pvt ", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }
    
    public List<SalesModel>? GetSalesByNameAndYear(string name, string year){
        try{
            var result = _connection.GetResultsFromQuery<SalesModel>(
                "SELECT " +
                " pvt.[SalesPersonID], pvt.[FullName], pvt.[JobTitle], pvt.[SalesTerritory], pvt.[Group], pvt.[SalesQuota]," + 
                $" pvt.[SalesYTD], pvt.[SalesLastYear], pvt.[{year}] " +
                "FROM (SELECT " + 
                "soh.[SalesPersonID], p.[FirstName] + ' ' + COALESCE(p.[MiddleName], '') + ' ' + p.[LastName] AS [FullName]," +
                " e.[JobTitle], st.[Name] AS [SalesTerritory], st.[Group], sp.[SalesQuota], sp.[SalesYTD], sp.[SalesLastYear]," +
                " soh.[SubTotal], YEAR(DATEADD(m, 6, soh.[OrderDate])) AS [FiscalYear] " +
                "FROM [Sales].[SalesPerson] sp " +
                "INNER JOIN [Sales].[SalesOrderHeader] soh " + 
                "ON sp.[BusinessEntityID] = soh.[SalesPersonID] " +
                "INNER JOIN [Sales].[SalesTerritory] st " +
                "ON sp.[TerritoryID] = st.[TerritoryID] " +
                "INNER JOIN [HumanResources].[Employee] e " +
                "ON soh.[SalesPersonID] = e.[BusinessEntityID] " +
                "INNER JOIN [Person].[Person] p " +
                "ON p.[BusinessEntityID] = sp.[BusinessEntityID] "+
                ") AS soh " +
                $"PIVOT ( SUM([SubTotal]) FOR [FiscalYear] IN ([{year}]) ) AS pvt " +
                $"WHERE pvt.[FullName] LIKE '%{name}%' AND pvt.[{year}] IS NOT NULL;", Map);           
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
            sales.SalesQuota = record["SalesQuota"] as decimal? ;
            sales.SalesYTD = record["SalesYTD"] as decimal?;
            sales.SalesLastYear = record["SalesLastYear"] as decimal?;
            sales.YearSelected = record.GetName(record.FieldCount - 1);
            sales.YearSelectedValue = record.GetValue(record.FieldCount - 1) as decimal?;


        return sales;
            
    }
}