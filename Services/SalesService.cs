
using System.Data;

public class SalesService : BaseSalesService
{
    private DataClient _connection;
    public SalesService(DataClient connection)
    {
        _connection = connection;
    }
    public List<SaleModel>? GetOverviewByPersons()
    {

        string sqlQuery = @"
    SELECT 
        P.FirstName + ' ' + P.LastName AS SalesPerson,
        ROUND(SUM(SOH.TotalDue), 2) AS TotalSalesAmount, 
        COUNT(DISTINCT SOH.SalesOrderID) AS TotalOrders
    FROM 
        Sales.SalesOrderHeader SOH
    INNER JOIN 
        Sales.SalesPerson SP ON SOH.SalesPersonID = SP.BusinessEntityID
    INNER JOIN 
        Person.Person P ON SP.BusinessEntityID = P.BusinessEntityID
    GROUP BY 
        P.FirstName, P.LastName
    ORDER BY 
        TotalSalesAmount DESC;
";
        try
        {
            var result = _connection.GetResultsFromQuery<SaleModel>(sqlQuery, MapWithOutYear);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<SaleModel>? GetSalesByPersonAndYear(string person, int? year)
    {
        string queryYear = year.HasValue ? $"AND YEAR(SOH.OrderDate) = {year}" : "";
        string sqlQuery = @"
    SELECT 
        P.FirstName + ' ' + P.LastName AS SalesPerson,
        YEAR(SOH.OrderDate) AS SalesYear,
        ROUND(SUM(SOH.TotalDue), 2) AS TotalSalesAmount,
        COUNT(DISTINCT SOH.SalesOrderID) AS TotalOrders
    FROM 
        Sales.SalesOrderHeader SOH
    INNER JOIN 
        Sales.SalesPerson SP ON SOH.SalesPersonID = SP.BusinessEntityID
    INNER JOIN 
        Person.Person P ON SP.BusinessEntityID = P.BusinessEntityID
    WHERE  " +
        $"P.FirstName + ' ' + P.LastName LIKE '%{person}%' " +
       queryYear +
    @"GROUP BY 
        P.FirstName, P.LastName, YEAR(SOH.OrderDate)
    ORDER BY 
        SalesYear, TotalSalesAmount DESC;
";

        try
        {
            var result = _connection.GetResultsFromQuery<SaleModel>(sqlQuery, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    private SaleModel Map(IDataRecord record)
    {
        return new SaleModel
        {
            SalesPerson = record["SalesPerson"] as string,
            TotalSalesAmount = (decimal)record["TotalSalesAmount"],
            TotalOrders = (int)record["TotalOrders"],
            SalesYear = (int?)record["SalesYear"]
        };
    }
    private SaleModel MapWithOutYear(IDataRecord record)
    {
        return new SaleModel
        {
            SalesPerson = record["SalesPerson"] as string,
            TotalSalesAmount = (decimal)record["TotalSalesAmount"],
            TotalOrders = (int)record["TotalOrders"]
        };
    }
}