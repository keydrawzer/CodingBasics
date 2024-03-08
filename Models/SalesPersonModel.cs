public class SalesPersonModel
{
    public int BusinessEntityID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }
    public string? SalesTerritory { get; set; }
    public int? SalesCount { get; set; }
    public decimal? SalesYTD { get; set; }
    public decimal? SalesQuota { get; set; }
    public decimal? SalesTotal { get; set; }
}