using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalesService 
{
    private readonly DataClient _connection;

    public SalesService(DataClient connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public List<SalesOverModel>? GetAll()
    {
        try
        {
            string query = @"SELECT YEAR(SOH.OrderDate) AS Year,
                                    COUNT(DISTINCT SOH.SalesOrderID) AS TotalOrders,
                                    SUM(SOD.LineTotal) AS TotalSalesAmount
                             FROM Sales.SalesOrderHeader SOH
                             INNER JOIN Sales.SalesOrderDetail SOD ON SOH.SalesOrderID = SOD.SalesOrderID
                             GROUP BY YEAR(SOH.OrderDate);";

            var result = _connection.GetResultsFromQuery<SalesOverModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving sales data: {ex.Message}");
        }
        return null;
    }

    public SalesOverModel Map(IDataRecord record)
    {
        return new SalesOverModel
        {
            Year = (int)record["Year"],
            TotalOrders = (int)record["TotalOrders"],
            TotalSalesAmount = (decimal)record["TotalSalesAmount"]
        };
    }
}
