namespace CodingBasics.Models
{
    // Represents sales summary information for a salesperson within a specific year
    public class SalesOverviewItem
    {
        public int SalesPersonID { get; set; }
        public string? SalesPersonName { get; set; }
        public decimal TotalSales { get; set; }
        public decimal SalesQuota { get; set; }
        public decimal SalesYTD { get; set; }
        public decimal SalesLastYear { get; set; }
        public string? OrderDate { get; set; }
        public int SaleYear { get; set; }
    }
}
