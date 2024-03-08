using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingBasics.Models
{
    [Table("SalesOrderHeader", Schema ="Sales")]
    public class SalesOrderHeader
    {
        [Key]
        public int SalesOrderID { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? DueDate {get; set;}
        public string? ShipDate {get; set;}
        public bool Status {get; set;}
        public bool OnlineOrderFlag {get; set;}
        public string? SalesOrderNumber {get; set;}
        public string? PurchaseOrderNumber {get; set;}
        public string? AccountNumber {get; set;}
        public int CustomerID {get; set;}
        public int SalesPersonID {get; set;}
        public int TerritoryID {get; set;}
        public int BillToAddressID {get; set;}
        public int ShipToAddressID {get; set;}
        public int ShipMethodID {get; set;}
        public int CreditCardID {get; set;}
        public string? CreditCardApprovalCode {get; set;}
        public int CurrencyRateID {get; set;}
        public decimal SubTotal {get; set;}
        public decimal TaxAmt {get; set;}
        public decimal Freight {get; set;}
        public decimal TotalDue {get; set;}
    }
}