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
        public DbSet<SalesPerson> SalesPersons { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

        // Configure the relationship between SalesOrderHeader and Employee entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderHeader>()
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(soh => soh.SalesPersonID);
        }
    }
}