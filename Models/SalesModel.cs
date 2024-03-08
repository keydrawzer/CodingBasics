public class SalesModel{
    public int SalesOrderID {get;set;}
    public DateTime OrderDate {get;set;}
    public DateTime DueDate {get;set;}
    public DateTime ShipDate {get;set;}
    public string? Status {get;set;}
    public int SalesOrderDetailID {get;set;}
    public int CustomerID {get;set;}
    public decimal SalesYTD {get;set;}
    public decimal SalesLastYear {get;set;}

}