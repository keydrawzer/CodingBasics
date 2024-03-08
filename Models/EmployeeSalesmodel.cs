public class EmployeeSalesModel
{
    // Campos de SalesModel
    public int SalesOrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ShipDate { get; set; }
    public string? Status { get; set; }
    public int SalesOrderDetailID { get; set; }
    public int CustomerID { get; set; }
    public decimal SalesYTD { get; set; }
    public decimal SalesLastYear { get; set; }

    // Campos de PersonModel
    public int BusinessEntityID { get; set; }
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
}