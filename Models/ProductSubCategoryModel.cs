using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingBasics.Models
{
    [Table("ProductSubcategory", Schema = "Production")]
    public class ProductSubcategory 
    {
        [Key]
        public int ProductSubcategoryID { get; set; }
        public int? ProductCategoryID { get; set; }
        public string? Name { get; set; }

    }
}