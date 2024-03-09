namespace CodingBasics.Models;

public partial class SalesByPersonAndYear
{
  public int SalesOrderId { get; set; }

  public string? FullName { get; set; }

  public string? JobTitle { get; set; }

  public string? SalesTerritory { get; set; }

  public decimal? SubTotal { get; set; }

  public string? Date { get; set; }
}
