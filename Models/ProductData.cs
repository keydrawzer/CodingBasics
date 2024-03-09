namespace CodingBasics.Models;

public partial class ProductData
{
  public int ProductId { get; set; }

  public string Name { get; set; } = null!;

  public string ProductModel { get; set; } = null!;

  public string? Category { get; set; }

  public string CultureId { get; set; } = null!;

  public string Description { get; set; } = null!;
}