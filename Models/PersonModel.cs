using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingBasics.Models
{
    [Table("Person", Schema = "Person")]
    public class Person
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public string? PersonType { get; set; }
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? MiddleName {get; set;}
    }
}