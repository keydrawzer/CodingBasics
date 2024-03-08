public class SalesOverviewModel
{
    public string? Territory { get; set; }
    public int? ShippedOrders { get; set; }
    public int? CancelledOrders { get; set; }
    public int? InPersonOrders { get; set; }
    public int? OnlineOrders { get; set; }
    public int? OrderedQuantity { get; set; }
    public decimal? ShippingCost { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? TotalDue { get; set; }
}