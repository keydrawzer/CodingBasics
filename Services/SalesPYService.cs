using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalesPYService
{
   private DataClient _connection;

    public SalesPYService(DataClient connection)
    {
        _connection = connection;
    }

    public List<SalesPYModel> GetSalesByPersonAndYear(string salesPersonName, int year)
    {
        try
        {
            var result = _connection.GetResultsFromQuery<SalesPYModel>(
                "SELECT sp.BusinessEntityID AS SalesPersonID ," +
                                  $"p.FirstName AS SalesPersonName, " +
                                  $"YEAR(soh.OrderDate) AS Year, " +
                                  $"COUNT(DISTINCT soh.SalesOrderID) AS TotalOrders, " +
                                  $"SUM(sod.LineTotal) AS TotalSalesAmount " +
                           $"FROM Sales.SalesOrderHeader soh " +
                           $"INNER JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID " +
                           $"INNER JOIN Sales.SalesPerson sp ON soh.SalesPersonID = sp.BusinessEntityID " +
                           $"INNER JOIN Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID " +
                           $"WHERE p.FirstName LIKE '%{salesPersonName}%' " +
                           $"AND YEAR(soh.OrderDate) = '{year}' " +
                           "GROUP BY sp.BusinessEntityID, p.FirstName, YEAR(soh.OrderDate);", Map);
             return result ?? new List<SalesPYModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return new List<SalesPYModel>();
    }

    public SalesPYModel Map(IDataRecord record)
    {
        SalesPYModel salespy = new SalesPYModel();
        salespy.SalesPersonID = (int) record["SalesPersonID"];
        salespy.SalesPersonName = record["SalesPersonName"] as string;
        salespy.Year = (int) record["Year"];
        salespy.TotalOrders = (int) record["TotalOrders"];
        salespy.TotalSalesAmount = (decimal) record["TotalSalesAmount"];

        return salespy;
    }
}