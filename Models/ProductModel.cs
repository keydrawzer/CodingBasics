using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingBasics.Models 
{
    [Table("Product", Schema = "Production")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        // public bool MakeFlag { get; set; }
        // public bool FinishedGoodsFlag { get; set; }
        public string? Color { get; set; }
        public short SafetyStockLevel { get; set; }
        // public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public int? ProductSubcategoryID { get; set; }


    }
}