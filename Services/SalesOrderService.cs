using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalesOrderService
{
    private DataClient _connection;
    public SalesOrderService(DataClient connection){
        _connection = connection;}

    public List<SalesOrderModel>? GetSalesOrderByTotal(){
    try{
        var result = _connection.GetResultsFromQuery<SalesOrderModel>(
           "select"+ 
         "sum(SubTotal) Subtotal,"+
         "sum(TaxAmt) Tax,"+
         "sum(Freight) Freight,"+
         "sum(TotalDue) Total"+ 
         "from sales.SalesOrderHeader",Map);
 return result;
}
        catch (Exception ex){
            Console.WriteLine($"Error message: {ex.Message}");
        }
        return null; }

        public SalesOrderModel Map(IDataRecord record){
         SalesOrderModel Sales = new SalesOrderModel();

         Sales.Subtotal = (float)record["Subtotal"];

         Sales.Tax = (float)record["Tax"];

         Sales.Freight = (float)record["Total"];

         return Sales;

    }

}