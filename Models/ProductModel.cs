public class ProductModel
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public  string? ProductNumber { get; set; }
    public bool MakeFlag { get; set; }
    public bool FinishedGoodsFlag { get; set; }
    public string? Color { get; set; }
    public short SafetyStockLevel { get; set; }
    public short ReorderPoint { get; set; }
    public decimal StandardCost { get; set; }
    public decimal ListPrice { get; set; }


}