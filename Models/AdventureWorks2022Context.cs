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

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductSubcategory> ProductSubcategories { get; set; }

    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

    public virtual DbSet<SalesPerson> SalesPeople { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=AdventureWorks2022;User Id=sa;Password=Passw0rd;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.BusinessEntityId).HasName("PK_Person_BusinessEntityID");

            entity.ToTable("Person", "Person", tb =>
                {
                    tb.HasComment("Human beings involved with AdventureWorks: employees, customer contacts, and vendor contacts.");
                    tb.HasTrigger("iuPerson");
                });

            entity.HasIndex(e => e.Rowguid, "AK_Person_rowguid").IsUnique();

            entity.HasIndex(e => new { e.LastName, e.FirstName, e.MiddleName }, "IX_Person_LastName_FirstName_MiddleName");

            entity.HasIndex(e => e.AdditionalContactInfo, "PXML_Person_AddContact");

            entity.HasIndex(e => e.Demographics, "PXML_Person_Demographics");

            entity.HasIndex(e => e.Demographics, "XMLPATH_Person_Demographics");

            entity.HasIndex(e => e.Demographics, "XMLPROPERTY_Person_Demographics");

            entity.HasIndex(e => e.Demographics, "XMLVALUE_Person_Demographics");

            entity.Property(e => e.BusinessEntityId)
                .ValueGeneratedNever()
                .HasComment("Primary key for Person records.")
                .HasColumnName("BusinessEntityID");
            entity.Property(e => e.AdditionalContactInfo)
                .HasComment("Additional contact information about the person stored in xml format. ")
                .HasColumnType("xml");
            entity.Property(e => e.Demographics)
                .HasComment("Personal information such as hobbies, and income collected from online shoppers. Used for sales analysis.")
                .HasColumnType("xml");
            entity.Property(e => e.EmailPromotion).HasComment("0 = Contact does not wish to receive e-mail promotions, 1 = Contact does wish to receive e-mail promotions from AdventureWorks, 2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners. ");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasComment("First name of the person.");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasComment("Last name of the person.");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasComment("Middle name or middle initial of the person.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.NameStyle).HasComment("0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.");
            entity.Property(e => e.PersonType)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("Primary type of person: SC = Store Contact, IN = Individual (retail) customer, SP = Sales person, EM = Employee (non-sales), VC = Vendor contact, GC = General contact");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.Suffix)
                .HasMaxLength(10)
                .HasComment("Surname suffix. For example, Sr. or Jr.");
            entity.Property(e => e.Title)
                .HasMaxLength(8)
                .HasComment("A courtesy title. For example, Mr. or Ms.");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Product_ProductID");

            entity.ToTable("Product", "Production", tb => tb.HasComment("Products sold or used in the manfacturing of sold products."));

            entity.HasIndex(e => e.Name, "AK_Product_Name").IsUnique();

            entity.HasIndex(e => e.ProductNumber, "AK_Product_ProductNumber").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_Product_rowguid").IsUnique();

            entity.Property(e => e.ProductId)
                .HasComment("Primary key for Product records.")
                .HasColumnName("ProductID");
            entity.Property(e => e.Class)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("H = High, M = Medium, L = Low");
            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .HasComment("Product color.");
            entity.Property(e => e.DaysToManufacture).HasComment("Number of days required to manufacture the product.");
            entity.Property(e => e.DiscontinuedDate)
                .HasComment("Date the product was discontinued.")
                .HasColumnType("datetime");
            entity.Property(e => e.FinishedGoodsFlag)
                .HasDefaultValue(true)
                .HasComment("0 = Product is not a salable item. 1 = Product is salable.");
            entity.Property(e => e.ListPrice)
                .HasComment("Selling price.")
                .HasColumnType("money");
            entity.Property(e => e.MakeFlag)
                .HasDefaultValue(true)
                .HasComment("0 = Product is purchased, 1 = Product is manufactured in-house.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Name of the product.");
            entity.Property(e => e.ProductLine)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("R = Road, M = Mountain, T = Touring, S = Standard");
            entity.Property(e => e.ProductModelId)
                .HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.")
                .HasColumnName("ProductModelID");
            entity.Property(e => e.ProductNumber)
                .HasMaxLength(25)
                .HasComment("Unique product identification number.");
            entity.Property(e => e.ProductSubcategoryId)
                .HasComment("Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID. ")
                .HasColumnName("ProductSubcategoryID");
            entity.Property(e => e.ReorderPoint).HasComment("Inventory level that triggers a purchase order or work order. ");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.SafetyStockLevel).HasComment("Minimum inventory quantity. ");
            entity.Property(e => e.SellEndDate)
                .HasComment("Date the product was no longer available for sale.")
                .HasColumnType("datetime");
            entity.Property(e => e.SellStartDate)
                .HasComment("Date the product was available for sale.")
                .HasColumnType("datetime");
            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .HasComment("Product size.");
            entity.Property(e => e.SizeUnitMeasureCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Unit of measure for Size column.");
            entity.Property(e => e.StandardCost)
                .HasComment("Standard cost of the product.")
                .HasColumnType("money");
            entity.Property(e => e.Style)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("W = Womens, M = Mens, U = Universal");
            entity.Property(e => e.Weight)
                .HasComment("Product weight.")
                .HasColumnType("decimal(8, 2)");
            entity.Property(e => e.WeightUnitMeasureCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Unit of measure for Weight column.");

            entity.HasOne(d => d.ProductSubcategory).WithMany(p => p.Products).HasForeignKey(d => d.ProductSubcategoryId);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK_ProductCategory_ProductCategoryID");

            entity.ToTable("ProductCategory", "Production", tb => tb.HasComment("High-level product categorization."));

            entity.HasIndex(e => e.Name, "AK_ProductCategory_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_ProductCategory_rowguid").IsUnique();

            entity.Property(e => e.ProductCategoryId)
                .HasComment("Primary key for ProductCategory records.")
                .HasColumnName("ProductCategoryID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Category description.");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductSubcategory>(entity =>
        {
            entity.HasKey(e => e.ProductSubcategoryId).HasName("PK_ProductSubcategory_ProductSubcategoryID");

            entity.ToTable("ProductSubcategory", "Production", tb => tb.HasComment("Product subcategories. See ProductCategory table."));

            entity.HasIndex(e => e.Name, "AK_ProductSubcategory_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_ProductSubcategory_rowguid").IsUnique();

            entity.Property(e => e.ProductSubcategoryId)
                .HasComment("Primary key for ProductSubcategory records.")
                .HasColumnName("ProductSubcategoryID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Subcategory description.");
            entity.Property(e => e.ProductCategoryId)
                .HasComment("Product category identification number. Foreign key to ProductCategory.ProductCategoryID.")
                .HasColumnName("ProductCategoryID");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.ProductSubcategories)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId).HasName("PK_SalesOrderHeader_SalesOrderID");

            entity.ToTable("SalesOrderHeader", "Sales", tb =>
                {
                    tb.HasComment("General sales order information.");
                    tb.HasTrigger("uSalesOrderHeader");
                });

            entity.HasIndex(e => e.SalesOrderNumber, "AK_SalesOrderHeader_SalesOrderNumber").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_SalesOrderHeader_rowguid").IsUnique();

            entity.HasIndex(e => e.CustomerId, "IX_SalesOrderHeader_CustomerID");

            entity.HasIndex(e => e.SalesPersonId, "IX_SalesOrderHeader_SalesPersonID");

            entity.Property(e => e.SalesOrderId)
                .HasComment("Primary key.")
                .HasColumnName("SalesOrderID");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(15)
                .HasComment("Financial accounting number reference.");
            entity.Property(e => e.BillToAddressId)
                .HasComment("Customer billing address. Foreign key to Address.AddressID.")
                .HasColumnName("BillToAddressID");
            entity.Property(e => e.Comment)
                .HasMaxLength(128)
                .HasComment("Sales representative comments.");
            entity.Property(e => e.CreditCardApprovalCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("Approval code provided by the credit card company.");
            entity.Property(e => e.CreditCardId)
                .HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.")
                .HasColumnName("CreditCardID");
            entity.Property(e => e.CurrencyRateId)
                .HasComment("Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.")
                .HasColumnName("CurrencyRateID");
            entity.Property(e => e.CustomerId)
                .HasComment("Customer identification number. Foreign key to Customer.BusinessEntityID.")
                .HasColumnName("CustomerID");
            entity.Property(e => e.DueDate)
                .HasComment("Date the order is due to the customer.")
                .HasColumnType("datetime");
            entity.Property(e => e.Freight)
                .HasComment("Shipping cost.")
                .HasColumnType("money");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.OnlineOrderFlag)
                .HasDefaultValue(true)
                .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Dates the sales order was created.")
                .HasColumnType("datetime");
            entity.Property(e => e.PurchaseOrderNumber)
                .HasMaxLength(25)
                .HasComment("Customer purchase order number reference. ");
            entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.SalesOrderNumber)
                .HasMaxLength(25)
                .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
                .HasComment("Unique sales order identification number.");
            entity.Property(e => e.SalesPersonId)
                .HasComment("Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.")
                .HasColumnName("SalesPersonID");
            entity.Property(e => e.ShipDate)
                .HasComment("Date the order was shipped to the customer.")
                .HasColumnType("datetime");
            entity.Property(e => e.ShipMethodId)
                .HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.")
                .HasColumnName("ShipMethodID");
            entity.Property(e => e.ShipToAddressId)
                .HasComment("Customer shipping address. Foreign key to Address.AddressID.")
                .HasColumnName("ShipToAddressID");
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)1)
                .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");
            entity.Property(e => e.SubTotal)
                .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.")
                .HasColumnType("money");
            entity.Property(e => e.TaxAmt)
                .HasComment("Tax amount.")
                .HasColumnType("money");
            entity.Property(e => e.TerritoryId)
                .HasComment("Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.")
                .HasColumnName("TerritoryID");
            entity.Property(e => e.TotalDue)
                .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false)
                .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.")
                .HasColumnType("money");

            entity.HasOne(d => d.SalesPerson).WithMany(p => p.SalesOrderHeaders).HasForeignKey(d => d.SalesPersonId);
        });

        modelBuilder.Entity<SalesPerson>(entity =>
        {
            entity.HasKey(e => e.BusinessEntityId).HasName("PK_SalesPerson_BusinessEntityID");

            entity.ToTable("SalesPerson", "Sales", tb => tb.HasComment("Sales representative current information."));

            entity.HasIndex(e => e.Rowguid, "AK_SalesPerson_rowguid").IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .ValueGeneratedNever()
                .HasComment("Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID")
                .HasColumnName("BusinessEntityID");
            entity.Property(e => e.Bonus)
                .HasComment("Bonus due if quota is met.")
                .HasColumnType("money");
            entity.Property(e => e.CommissionPct)
                .HasComment("Commision percent received per sale.")
                .HasColumnType("smallmoney");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.SalesLastYear)
                .HasComment("Sales total of previous year.")
                .HasColumnType("money");
            entity.Property(e => e.SalesQuota)
                .HasComment("Projected yearly sales.")
                .HasColumnType("money");
            entity.Property(e => e.SalesYtd)
                .HasComment("Sales total year to date.")
                .HasColumnType("money")
                .HasColumnName("SalesYTD");
            entity.Property(e => e.TerritoryId)
                .HasComment("Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.")
                .HasColumnName("TerritoryID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
