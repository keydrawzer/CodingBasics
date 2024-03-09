using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalesService
{
    private readonly DataClient _connection;

    public SalesService(DataClient connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public List<SaleModel>? GetAll()
    {
        return ExecuteQuery("SELECT * FROM [Sales].[vSales]");
    }

    public List<SaleModel>? GetSalesByName(string name)
    {
        string query = $"SELECT * FROM [AdventureWorks2022].[Sales].[vSales] WHERE CONCAT(FirstName, ' ', MiddleName, ' ', LastName) LIKE '%{name}%'";
        return ExecuteQuery(query);
    }

    public List<SaleModel>? GetSalesByNameAndYear(string name, string year)
    {
        string query = $"SELECT * FROM [AdventureWorks2022].[Sales].[vSales] WHERE CONCAT(FirstName, ' ', MiddleName, ' ', LastName) LIKE '%{name}%' AND OrderDate LIKE '%{year}%'";
        return ExecuteQuery(query);
    }

    private List<SaleModel>? ExecuteQuery(string query)
    {
        try
        {
            return _connection.GetResultsFromQuery<SaleModel>(query, Map);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
            return null;
        }
    }

    private SaleModel Map(IDataRecord record)
    {
        SaleModel sale = new SaleModel
        {
            SalesOrderID = (int)record["SalesOrderID"],
            CarrierTrackingNumber = record["CarrierTrackingNumber"] as string,
            OrderQty = (short)record["OrderQty"],
            LineTotal = (decimal)record["LineTotal"],
            OrderDate = ((DateTime)record["OrderDate"]).ToString("dd-MM-yyyy"),
            ProductName = record["Name"] as string,
            FirstName = record["FirstName"] as string,
            MiddleName = record["MiddleName"] as string,
            LastName = record["LastName"] as string
        };
        return sale;
    }
}