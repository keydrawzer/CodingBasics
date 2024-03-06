
public class SalesWithSalesPersonModel
{
    public int? SalesOrderID { get; set; }
    public DateTime? OrderDate { get; set; }
    public int? SalesPersonID { get; set; }
    public string? SalesPersonName { get; set; }
    public int? ProductID { get; set; }
    public int? OrderQty { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? LineTotal { get; set; }
}
