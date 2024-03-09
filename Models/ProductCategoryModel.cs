using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingBasics.Models 
{
    [Table("ProductCategory", Schema ="Production")]
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public string? Name {get; set; }

        public string? CategoryType { get; set;}
    }
}