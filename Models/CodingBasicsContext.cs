using Microsoft.EntityFrameworkCore;
using CodingBasics.Models;
using System.ComponentModel;



namespace CodingBasics.Models 
{
    public class CodingBasicsContext : DbContext
    {
        public CodingBasicsContext(DbContextOptions<CodingBasicsContext> options) : base(options)
        { }
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }
      

    }
}