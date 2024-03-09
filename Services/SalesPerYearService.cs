using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalesPersonYearService
{
    private readonly DataClient _connection;

    public SalesPersonYearService(DataClient connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public List<SalesPersonYearModel> GetSalesByPersonAndYear(string salesPersonName, int year)
    {
        try
        {
            string query = $@"
                SELECT
                    SP.BusinessEntityID AS SalesPersonID,
                    P.FirstName AS SalesPersonName,
                    YEAR(SOH.OrderDate) AS Year,
                    COUNT(DISTINCT SOH.SalesOrderID) AS TotalOrders,
                    SUM(SOD.LineTotal) AS TotalSalesAmount
                FROM
                    Sales.SalesOrderHeader SOH
                    INNER JOIN Sales.SalesOrderDetail SOD ON SOH.SalesOrderID = SOD.SalesOrderID
                    INNER JOIN Sales.SalesPerson SP ON SOH.SalesPersonID = SP.BusinessEntityID
                    INNER JOIN Person.Person P ON SP.BusinessEntityID = P.BusinessEntityID
                WHERE
                    (P.FirstName LIKE '%' + @salesPersonName + '%' OR @salesPersonName IS NULL)
                    AND YEAR(SOH.OrderDate) = @year
                GROUP BY
                    SP.BusinessEntityID, P.FirstName, YEAR(SOH.OrderDate);";

            var parameters = new { salesPersonName, year };

            var result = _connection.GetResultsFromQuery<SalesPersonYearModel>(query, Map, parameters);

            // Verifica si el resultado es nulo antes de devolverlo
            return result ?? new List<SalesPersonYearModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving sales data: {ex.Message}");
            return new List<SalesPersonYearModel>();
        }
    }

    public SalesPersonYearModel Map(IDataRecord record)
    {
        return new SalesPersonYearModel
        {
            SalesPersonID = (int)record["SalesPersonID"],
            SalesPersonName = record["SalesPersonName"] as string,
            Year = (int)record["Year"],
            TotalOrders = (int)record["TotalOrders"],
            TotalSalesAmount = (decimal)record["TotalSalesAmount"]
        };
    }
}
