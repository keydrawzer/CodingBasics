using CodingBasics.Features.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics;

public partial class CodingBasicsDbContext : DbContext
{
    public CodingBasicsDbContext(DbContextOptions<CodingBasicsDbContext> options) : base(options) { }

    public virtual DbSet<SalesOrderHeader> SalesOrdersHeaders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.SalesOrderID);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
