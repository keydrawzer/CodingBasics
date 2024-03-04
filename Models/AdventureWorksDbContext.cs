using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Models;

public partial class AdventureWorksDbContext : DbContext
{
    public AdventureWorksDbContext()
    {
    }

    public AdventureWorksDbContext(DbContextOptions<AdventureWorksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VEmployee> VEmployees { get; set; }

    public virtual DbSet<VProductAndDescription> VProductAndDescriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-I3D0B80O\\SQLEXPRESS;Database=AdventureWorks2022;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<VEmployee>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEmployee", "HumanResources");

            entity.Property(e => e.AdditionalContactInfo).HasColumnType("xml");
            entity.Property(e => e.AddressLine1).HasMaxLength(60);
            entity.Property(e => e.AddressLine2).HasMaxLength(60);
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.CountryRegionName).HasMaxLength(50);
            entity.Property(e => e.EmailAddress).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.JobTitle).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(25);
            entity.Property(e => e.PhoneNumberType).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(15);
            entity.Property(e => e.StateProvinceName).HasMaxLength(50);
            entity.Property(e => e.Suffix).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(8);
        });

        modelBuilder.Entity<VProductAndDescription>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductAndDescription", "Production");

            entity.Property(e => e.CultureId)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("CultureID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductModel).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
