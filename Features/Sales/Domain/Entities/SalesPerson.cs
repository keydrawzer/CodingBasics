namespace CodingBasics.Features.Sales.Domain.Entities;

public class SalesPerson
{
    public int BusinessEntityId { get; set; }
    public int TerritoryId { get; set; }
    public decimal SalesQuota { get; set; }
    public decimal Bonus { get; set; }
    public decimal CommissionPct { get; set; }
    public decimal SalesYTD { get; set; }
    public decimal SalesLastYear { get; set; }

}
