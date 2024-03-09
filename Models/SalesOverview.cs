namespace CodingBasics.Models;

public partial class SalesOverview
{
  public int SellerID { get; set; }

  public string? Seller { get; set; }

  public decimal? SalesLastYear { get; set; }

  public decimal? SalesQuota { get; set; }

  public int? OrdersCount { get; set; }

  public decimal? TotalSales { get; set; }
  public decimal? MaxOrderSale { get; set; }
  public decimal? MinOrderSale { get; set; }
  public string? SalesTerritory { get; set; }
}