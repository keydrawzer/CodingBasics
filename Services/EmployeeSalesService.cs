using System.Data;
using Microsoft.AspNetCore.Mvc;

public class EmployeeSalesService
{
    private DataClient _connection;
    public EmployeeSalesService(DataClient connection){
        _connection = connection;
    }
    public List<EmployeeSalesModel>? GetsalesByNameAndyear(string name, int year){
        try{
            var result = _connection.GetResultsFromQuery<EmployeeSalesModel>(
                "SELECT soh.SalesOrderID, soh.OrderDate, soh.DueDate, soh.ShipDate, soh.Status, "+
                "sod.SalesOrderDetailID, soh.CustomerID, sp.SalesYTD, sp.SalesLastYear, " +
                "pe.FirstName, pe.MiddleName, pe.LastName " +
                "FROM Sales.SalesOrderHeader soh " +
                "INNER JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID "+
                "LEFT JOIN Sales.SalesPerson sp ON soh.SalesPersonID = sp.BusinessEntityID "+
                "INNER JOIN HumanResources.Employee hr ON sp.BusinessEntityID = hr.BusinessEntityID " +
                "INNER JOIN Person.Person pe ON hr.BusinessEntityID =pe.BusinessEntityID " +
                $"WHERE " + 
                $"    ('{name}' ='' OR '{name}' IS NULL OR CONCAT(pe.FirstName, ' ', pe.MiddleName, ' ', pe.LastName) LIKE '%{name}%') " +
                $"    AND " +
                $"    ('{year}' = '' OR '{year}' IS NULL OR soh.OrderDate = '{year}')", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }


    public EmployeeSalesModel Map(IDataRecord record){
        EmployeeSalesModel Emple = new EmployeeSalesModel();
            Emple.SalesOrderID = (int)record["SalesOrderID"];
            Emple.OrderDate = (DateTime)record["OrderDate"];
            Emple.ShipDate = (DateTime)record["ShipDate"];
            Emple.Status = record["Status"] as string;
            Emple.SalesOrderDetailID = (int)record["SalesOrderDetailID"];
            Emple.CustomerID = (int)record["CustomerID"];
            Emple.SalesYTD = record.IsDBNull(record.GetOrdinal("SalesYTD")) ? 0m : record.GetDecimal(record.GetOrdinal("SalesYTD"));
            Emple.SalesLastYear = record.IsDBNull(record.GetOrdinal("SalesLastYear")) ? 0m : record.GetDecimal(record.GetOrdinal("SalesLastYear"));           
            Emple.BusinessEntityID = (int)record["BusinessEntityID"];
            Emple.Title = record["Title"] as string;
            Emple.FirstName = record["FirstName"] as string;
            Emple.MiddleName = record["MiddleName"] as string;
            return Emple;
    }
}