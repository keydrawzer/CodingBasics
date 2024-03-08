namespace CodingBasics.Models;

public partial class SalesByPersonAndYear
{
  public int SalesPersonId { get; set; }

  public string? FullName { get; set; }

  public string? JobTitle { get; set; }

  public string? SalesTerritory { get; set; }

  public decimal? TotalSales { get; set; }

  public int? Year { get; set; }
}
