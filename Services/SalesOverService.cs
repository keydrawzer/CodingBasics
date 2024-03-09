using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalesOverService 
{
        private DataClient _connection;
    public SalesOverService(DataClient connection){
        _connection = connection;
    }

        public List<SalesOverModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<SalesOverModel>("SELECT YEAR(soh.OrderDate) AS Year, " +
            "COUNT(DISTINCT soh.SalesOrderID) AS TotalOrders, " +
            "SUM(sod.LineTotal) AS TotalSalesAmount " +
            "FROM Sales.SalesOrderHeader soh " +
            "INNER JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID " +
            "GROUP BY YEAR(soh.OrderDate);", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public SalesOverModel Map(IDataRecord record)
{
    SalesOverModel sales = new SalesOverModel();
    sales.Year = (int)record["Year"];
    sales.TotalOrders = (int)record["TotalOrders"];
    sales.TotalSalesAmount = (decimal)record["TotalSalesAmount"];

    return sales;
}
}