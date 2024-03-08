using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalesService{

    private DataClient _connection;
    public SalesService(DataClient connection){
        _connection = connection;
    }

    public List<SalesModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<SalesModel>("SELECT soh.SalesOrderID, soh.OrderDate, soh.DueDate, soh.ShipDate, "+
            "soh.Status, sod.SalesOrderDetailID, soh.CustomerID, sp.SalesYTD, sp.SalesLastYear FROM Sales.SalesOrderHeader soh " +
            "INNER JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID " +
            "LEFT JOIN Sales.SalesPerson sp ON soh.SalesPersonID = sp.BusinessEntityID", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    } 
    

    public SalesModel Map(IDataRecord record){
        SalesModel sales = new SalesModel();
            sales.SalesOrderID = (int)record["SalesOrderID"];
            sales.OrderDate = (DateTime)record["OrderDate"];
            sales.DueDate = (DateTime)record["DueDate"]; 
            sales.ShipDate = (DateTime)record["ShipDate"];
            sales.Status = record["Status"] as string;
            sales.SalesOrderDetailID = (int)record["SalesOrderDetailID"];
            sales.CustomerID = (int)record["CustomerID"];
            sales.SalesYTD = record.IsDBNull(record.GetOrdinal("SalesYTD")) ? 0m : record.GetDecimal(record.GetOrdinal("SalesYTD"));
            sales.SalesLastYear = record.IsDBNull(record.GetOrdinal("SalesLastYear")) ? 0m : record.GetDecimal(record.GetOrdinal("SalesLastYear"));           
            return sales;   
    }
}