

public class SalesOverviewService
{
    private DataClient _connection;

    public SalesOverviewService(DataClient connection)
    {
        _connection = connection;
    }

    public async Task<IResult> GetGroupedByTerritory()
    {
        var query =
            "SELECT" +
                " st.Name AS 'Territory'" +
                " ,COUNT(case when soh.Status = 5 then 1 end) AS 'ShippedOrders'" +
                " ,COUNT(case when soh.Status = 6 then 1 end) AS 'CancelledOrders'" +
                " ,COUNT(case when soh.Status = 6 then 1 end) AS 'CancelledOrders'" +
                " ,COUNT(case when soh.OnlineOrderFlag = 1 then 1 end) AS 'InPersonOrders'" +
                " ,COUNT(case when soh.OnlineOrderFlag = 0 then 1 end) AS 'OnlineOrders'" +
                " ,SUM(sod.OrderQty) AS 'OrderedQuantity'" +
                " ,SUM(soh.Freight) AS 'ShippingCost'" +
                " ,SUM(soh.SubTotal) AS 'SubTotal'" +
                " ,SUM(soh.TaxAmt) AS 'TaxAmount'" +
                " ,SUM(soh.TotalDue) AS 'TotalDue'" +
            " FROM Sales.SalesOrderHeader soh" +
                " INNER JOIN Sales.SalesOrderDetail sod" +
                " ON soh.SalesOrderID = sod.SalesOrderID" +
                " INNER JOIN Sales.SalesTerritory st" +
                " ON soh.TerritoryID = st.TerritoryID" +
            " GROUP BY st.Name" +
            " ORDER BY st.Name";

        var result = await _connection.GetResultsFromQuery<SalesOverviewModel>(query);

        return TypedResults.Ok(result);
    }


}


