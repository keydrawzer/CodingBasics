using CodingBasics.Data;
using CodingBasics.Models;

namespace CodingBasics.Services
{
    public class SalesPersonService
    {
        private readonly BasicDbContext _context;
        public SalesPersonService(BasicDbContext context)
        {
            _context = context;
        }

        // This method retrieves a summary of sales for all salespeople.
        public IQueryable<SalesOverviewItem> GetSalesSummary()
        {
            try
            {
                // Join SalesOrderHeaders and Persons tables to get salesperson names and total sales
                var salesSummary = _context.SalesOrderHeaders
                    .Join(
                        _context.Persons,
                        soh => soh.SalesPersonID,
                        p => p.BusinessEntityID,
                        (soh, p) => new
                        {
                            SalesPersonName = p.FirstName + " " + p.LastName,
                            TotalSales = soh.TotalDue,
                        })
                    // Group data by salesperson name
                    .GroupBy(x => x.SalesPersonName)
                    // Select data for each group and create SalesOverviewItem objects
                    .Select(g => new SalesOverviewItem
                    {
                        SalesPersonName = g.Key,
                        TotalSales = g.Sum(x => x.TotalSales),
                    });

                return salesSummary;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JustError: {ex.Message}");
                return null;
            }
        }

        // This method retrieves sales data for a specific salesperson and year based on provided name and year.
        public IEnumerable<SalesOverviewItem> GetSalesForSalesPersonAndYear(string name, int year)
        {
            // Find salesperson IDs matching the provided name (case-insensitive search on first or last name).
            var salesPeople = _context.Persons
                .Where(sp => sp.FirstName.Contains(name) || sp.LastName.Contains(name))
                .Select(sp => sp.BusinessEntityID)
                .ToList();

            if (salesPeople.Any()) // Check if any salespeople were found
            {
                // Filter sales orders for the salespeople in the specified year and calculate total sales.
                var salesSummary = _context.SalesOrderHeaders
                    .Where(soh => salesPeople.Contains(soh.SalesPersonID) && soh.OrderDate.Year == year)
                    .Join(
                        _context.Employees,
                        soh => soh.SalesPersonID,
                        e => e.BusinessEntityID,
                        (soh, e) => new
                        {
                            SalesPersonID = soh.SalesPersonID,
                            SalesPersonName = e.FirstName + " " + e.LastName, // Use full name from Employees
                            Year = soh.OrderDate.Year,
                            TotalDue = soh.TotalDue
                        })
                    .GroupBy(x => new { x.SalesPersonID, x.Year, x.SalesPersonName }) // Group by salesperson ID, year, and name
                    .Select(g => new SalesOverviewItem
                    {
                        SalesPersonID = g.Key.SalesPersonID,
                        SalesPersonName = g.Key.SalesPersonName,
                        SaleYear = g.Key.Year,
                        TotalSales = g.Sum(soh => soh.TotalDue) // Calculate total sales using TotalDue
                    })
                    .ToList();

                return salesSummary;
            }
            else
            {
                // Return an empty list if no salespeople were found matching the name
                return Enumerable.Empty<SalesOverviewItem>();
            }
        }
    }
}
