using Microsoft.EntityFrameworkCore;
using CodingBasics.Models;
using System.ComponentModel;

namespace CodingBasics.Data
{
    public class BasicDbContext : DbContext
    {
        public BasicDbContext(DbContextOptions<BasicDbContext> options) : base(options)
        { }

        // Exposes model sets for interacting with specific tables in the database
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }

    }
}