using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Models;

public partial class AdventureWorks2022Context : DbContext
{
    public AdventureWorks2022Context()
    {
    }

    public AdventureWorks2022Context(DbContextOptions<AdventureWorks2022Context> options)
        : base(options)
    {
    }

    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
    public virtual DbSet<SalesPerson> SalesPeople { get; set; }
    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
=> optionsBuilder.UseSqlServer("Server=localhost;Database=AdventureWorks2022;User Id=sa;Password=Passw0rd;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId).HasName("PK_SalesOrderHeader_SalesOrderID");

            entity.ToTable("SalesOrderHeader", "Sales", tb =>
                {
                    tb.HasComment("General sales order information.");
                    tb.HasTrigger("uSalesOrderHeader");
                });

            entity.Property(e => e.SalesOrderId).HasComment("Primary key.");
            entity.Property(e => e.AccountNumber).HasComment("Financial accounting number reference.");
            entity.Property(e => e.BillToAddressId).HasComment("Customer billing address. Foreign key to Address.AddressID.");
            entity.Property(e => e.Comment).HasComment("Sales representative comments.");
            entity.Property(e => e.CreditCardApprovalCode).HasComment("Approval code provided by the credit card company.");
            entity.Property(e => e.CreditCardId).HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.");
            entity.Property(e => e.CurrencyRateId).HasComment("Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.");
            entity.Property(e => e.CustomerId).HasComment("Customer identification number. Foreign key to Customer.BusinessEntityID.");
            entity.Property(e => e.DueDate).HasComment("Date the order is due to the customer.");
            entity.Property(e => e.Freight).HasComment("Shipping cost.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");
            entity.Property(e => e.OnlineOrderFlag)
                .HasDefaultValue(true)
                .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Dates the sales order was created.");
            entity.Property(e => e.PurchaseOrderNumber).HasComment("Customer purchase order number reference. ");
            entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
            entity.Property(e => e.SalesOrderNumber)
                .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
                .HasComment("Unique sales order identification number.");
            entity.Property(e => e.SalesPersonId).HasComment("Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.");
            entity.Property(e => e.ShipDate).HasComment("Date the order was shipped to the customer.");
            entity.Property(e => e.ShipMethodId).HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");
            entity.Property(e => e.ShipToAddressId).HasComment("Customer shipping address. Foreign key to Address.AddressID.");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");
            entity.Property(e => e.SubTotal).HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");
            entity.Property(e => e.TaxAmt).HasComment("Tax amount.");
            entity.Property(e => e.TerritoryId).HasComment("Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.");
            entity.Property(e => e.TotalDue)
                .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false)
                        .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.");
        });

        modelBuilder.Entity<SalesPerson>(entity =>
               {
                   entity.HasKey(e => e.BusinessEntityId).HasName("PK_SalesPerson_BusinessEntityID");

                   entity.ToTable("SalesPerson", "Sales", tb => tb.HasComment("Sales representative current information."));

                   entity.Property(e => e.BusinessEntityId)
                       .ValueGeneratedNever()
                       .HasComment("Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID");
                   entity.Property(e => e.Bonus).HasComment("Bonus due if quota is met.");
                   entity.Property(e => e.CommissionPct).HasComment("Commision percent received per sale.");
                   entity.Property(e => e.ModifiedDate)
                       .HasDefaultValueSql("(getdate())")
                       .HasComment("Date and time the record was last updated.");
                   entity.Property(e => e.Rowguid)
                       .HasDefaultValueSql("(newid())")
                       .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
                   entity.Property(e => e.SalesLastYear).HasComment("Sales total of previous year.");
                   entity.Property(e => e.SalesQuota).HasComment("Projected yearly sales.");
                   entity.Property(e => e.SalesYtd).HasComment("Sales total year to date.");
                   entity.Property(e => e.TerritoryId).HasComment("Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.");
               });

        modelBuilder.Entity<Person>(entity =>
                {
                    entity.HasKey(e => e.BusinessEntityId).HasName("PK_Person_BusinessEntityID");

                    entity.ToTable("Person", "Person", tb =>
                        {
                            tb.HasComment("Human beings involved with AdventureWorks: employees, customer contacts, and vendor contacts.");
                            tb.HasTrigger("iuPerson");
                        });

                    entity.Property(e => e.BusinessEntityId)
                        .ValueGeneratedNever()
                        .HasComment("Primary key for Person records.");
                    entity.Property(e => e.AdditionalContactInfo).HasComment("Additional contact information about the person stored in xml format. ");
                    entity.Property(e => e.Demographics).HasComment("Personal information such as hobbies, and income collected from online shoppers. Used for sales analysis.");
                    entity.Property(e => e.EmailPromotion).HasComment("0 = Contact does not wish to receive e-mail promotions, 1 = Contact does wish to receive e-mail promotions from AdventureWorks, 2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners. ");
                    entity.Property(e => e.FirstName).HasComment("First name of the person.");
                    entity.Property(e => e.LastName).HasComment("Last name of the person.");
                    entity.Property(e => e.MiddleName).HasComment("Middle name or middle initial of the person.");
                    entity.Property(e => e.ModifiedDate)
                        .HasDefaultValueSql("(getdate())")
                        .HasComment("Date and time the record was last updated.");
                    entity.Property(e => e.NameStyle).HasComment("0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.");
                    entity.Property(e => e.PersonType)
                        .IsFixedLength()
                        .HasComment("Primary type of person: SC = Store Contact, IN = Individual (retail) customer, SP = Sales person, EM = Employee (non-sales), VC = Vendor contact, GC = General contact");
                    entity.Property(e => e.Rowguid)
                        .HasDefaultValueSql("(newid())")
                        .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
                    entity.Property(e => e.Suffix).HasComment("Surname suffix. For example, Sr. or Jr.");
                    entity.Property(e => e.Title).HasComment("A courtesy title. For example, Mr. or Ms.");
                });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
