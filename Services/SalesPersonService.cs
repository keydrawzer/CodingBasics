using Microsoft.Data.SqlClient;

public class SalesPersonService
{
    private DataClient _connection;

    public SalesPersonService(DataClient connection)
    {
        _connection = connection;
    }

    public async Task<IResult> GetAll()
    {
        var query =
            "SELECT " +
                "sp.[BusinessEntityID]" +
                ",sp.[FirstName] " +
                ",sp.[LastName] " +
                ",sp.[JobTitle] " +
                ",sp.[TerritoryName] AS [SalesTerritory] " +
                ",COUNT(soh.SalesOrderID) AS 'SalesCount' " +
                ",sp.[SalesYTD] " +
                ", sp.[SalesQuota] " +
                ",SUM(soh.[SubTotal]) AS 'SalesTotal' " +
            "FROM [Sales].[vSalesPerson] sp " +
                "INNER JOIN [Sales].[SalesOrderHeader] soh " +
                "ON sp.[BusinessEntityID] = soh.[SalesPersonID] " +
            "GROUP BY " +
                " sp.[BusinessEntityID] " +
                ",sp.[FirstName] " +
                ",sp.[LastName] " +
                ",sp.[JobTitle] " +
                ",sp.[TerritoryName] " +
                ",sp.[SalesQuota] " +
                ",sp.[SalesYTD] " +
            "ORDER BY sp.[BusinessEntityID]";

        var result = await _connection.GetResultsFromQuery<SalesPersonModel>(query);

        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetByNameAndYear(string name, string year)
    {
        bool isYearInteger = int.TryParse(year, out int _);

        if (!isYearInteger && year != null && year != string.Empty)
        {
            throw new BadHttpRequestException("The request was invalid. Please provide a valid year in the 'year' parameter. The 'year' parameter can be left empty or with integers.");
        }

        SqlParameter[] queryParameters =
        {
            new("@Name", name),
            new("@NameSearch", "%" + name + "%"),
            new("@Year", year)
        };

        string query =
            "SELECT " +
                "sp.[BusinessEntityID] " +
                ",sp.[FirstName] " +
                ",sp.[LastName] " +
                ",sp.[JobTitle] " +
                ",sp.[TerritoryName] AS [SalesTerritory] " +
                ",COUNT(soh.SalesOrderID) AS 'Orders Count' " +
                ",sp.[SalesYTD] " +
                ",sp.[SalesQuota] " +
                ",SUM(soh.[SubTotal]) AS 'SalesTotal' " +
            "FROM [Sales].[vSalesPerson] sp " +
                "INNER JOIN [Sales].[SalesOrderHeader] soh " +
                "ON sp.[BusinessEntityID] = soh.[SalesPersonID] " +
            "WHERE (YEAR(soh.OrderDate) = @Year OR @Year IS NULL OR @Year = '') " +
                "AND " +
                "(@Name ='' OR @Name IS NULL OR CONCAT(sp.FirstName, ' ', sp.MiddleName, ' ', sp.LastName) LIKE @NameSearch) " +
            "GROUP BY " +
                "sp.[BusinessEntityID] " +
                ",sp.[FirstName] " +
                ",sp.[LastName] " +
                ",sp.[JobTitle] " +
                ",sp.[TerritoryName] " +
                ",sp.[SalesQuota] " +
                ",sp.[SalesYTD]" +
            "ORDER BY sp.[BusinessEntityID]";

        var result = await _connection.GetResultsFromQuery<SalesPersonModel>(query, queryParameters);

        return TypedResults.Ok(result);
    }
}


