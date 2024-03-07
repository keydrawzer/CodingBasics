using Microsoft.EntityFrameworkCore;
using CodingBasics.Models;

namespace CodingBasics.Data
{
    public class BasicDbContext : DbContext
    {
        public BasicDbContext(DbContextOptions<BasicDbContext> options) : base(options)
        { }

        // Exposes model sets for interacting with specific tables in the database
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}