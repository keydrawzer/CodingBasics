using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;

public class SalesService
{

    private DataClient _connection;

    public SalesService(DataClient connection)
    {
        _connection = connection;
    }


    public List<SalesWithSalesPersonModel> GetSalesWithSalesPerson()
    {
        string sqlQuery = @"
                SELECT 
    soh.SalesOrderID,
    soh.OrderDate,
    sp.BusinessEntityID AS SalesPersonID,
    p.FirstName + ' ' + p.LastName AS SalesPersonName,
    sod.ProductID,
    sod.OrderQty,
    sod.UnitPrice,
    sod.LineTotal
    FROM 
    Sales.SalesOrderHeader soh
    INNER JOIN 
    Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID         INNER JOIN 
    Sales.SalesPerson sp ON soh.SalesPersonID = sp.BusinessEntityID
    INNER JOIN 
    Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID";

        var salesWithSalesPersonList = _connection.GetResultsFromQuery(sqlQuery, ParseSalesData);
        return salesWithSalesPersonList;
    }



    public SalesWithSalesPersonModel ParseSalesData(IDataRecord record)
    {
        return new SalesWithSalesPersonModel
        {
            SalesOrderID = record["SalesOrderID"] as int?,
            OrderDate = record.IsDBNull(record.GetOrdinal("OrderDate")) ? null : (DateTime?)record["OrderDate"],
            SalesPersonID = record["SalesPersonID"] as int?,
            SalesPersonName = record["SalesPersonName"] as string,
            ProductID = record["ProductID"] as int?,
            OrderQty = record["OrderQty"] as int?,
            UnitPrice = record.IsDBNull(record.GetOrdinal("UnitPrice")) ? null : (decimal?)record["UnitPrice"],
            LineTotal = record.IsDBNull(record.GetOrdinal("LineTotal")) ? null : (decimal?)record["LineTotal"]
        };
    }

}
