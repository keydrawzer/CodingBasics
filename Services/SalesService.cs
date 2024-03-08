

namespace CodingBasics.Services
{
    using System;
    using System.Linq;
    using CodingBasics.Models;

    public class SalesSummary
    {
        public string SalesPersonName { get; set; }

        public decimal TotalSales { get; set; }
    }
    public class SalesService
    {
        private readonly AdventureWorks2022Context _context;

        public SalesService(AdventureWorks2022Context context)
        {
            _context = context;
        }

        public IQueryable<SalesSummary> GetSalesSummaryBySalesPerson()
        {
            var salesSummary = _context.SalesOrderHeaders
                .Join(
                    _context.People,
                    soh => soh.SalesPersonId,
                    p => p.BusinessEntityId,
                    (soh, p) => new
                    {
                        SalesPersonName = p.FirstName + " " + p.LastName,
                        TotalSales = soh.TotalDue,
                    })
                .GroupBy(x => x.SalesPersonName)
                .Select(g => new SalesSummary
                {
                    SalesPersonName = g.Key,
                    TotalSales = g.Sum(x => x.TotalSales),
                });

            return salesSummary;
        }


        public decimal GetSalesForSalesPerson(string salesPersonName, int year)
        {
            int? salesPersonId = _context.People
             .Where(sp => (sp.FirstName + " " + sp.LastName) == salesPersonName)
             .Select(sp => (int?)sp.BusinessEntityId)
             .FirstOrDefault();

            if (salesPersonId.HasValue)
            {
                decimal totalSales = _context.SalesOrderHeaders
                    .Where(soh => soh.SalesPersonId == salesPersonId &&
                                   soh.OrderDate.Year == year)
                    .Sum(soh => soh.TotalDue);

                return totalSales;
            }
            else
            {
                throw new ArgumentException("Sales person not found.", nameof(salesPersonName));
            }
        }
    }
}
