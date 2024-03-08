namespace CodingBasics.Features.Sales.Domain.Entities;

public class SalesOrderHeader
{
    public int SalesOrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DueDate { get; set; }
    public string? SalesOrderNumber { get; set; }
    public int? SalesPersonID { get; set; }
    public string? CreditCardApprovalCode { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Freight { get; set; }
    public decimal TotalDue { get; set; }
}
