using Microsoft.AspNetCore.Mvc;
using CodingBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Controllers
{
  [Route("api/sales")]
  [ApiController]
  public class SaleController : ControllerBase
  {
    private readonly AdventureWorks2022Context _context;

    public SaleController(AdventureWorks2022Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("")]
    public ActionResult<IEnumerable<SalesOverview>> GetSalesOverview()
    {
      var result = _context.SalesPeople
    .Join(
        _context.SalesOrderHeaders,
        sp => sp.BusinessEntityId,
        soh => soh.SalesPersonId,
        (sp, soh) => new { SalesPerson = sp, SalesOrderHeader = soh })
    .Join(
        _context.SalesTerritories,
        joined => joined.SalesPerson.TerritoryId,
        st => st.TerritoryId,
        (joined, st) => new { joined.SalesPerson, joined.SalesOrderHeader, SalesTerritory = st })
    .Join(
        _context.Employees,
        joined => joined.SalesOrderHeader.SalesPersonId,
        e => e.BusinessEntityId,
        (joined, e) => new { joined.SalesPerson, joined.SalesOrderHeader, joined.SalesTerritory, Employee = e })
    .Join(
        _context.Persons,
        joined => joined.SalesPerson.BusinessEntityId,
        p => p.BusinessEntityId,
        (joined, p) => new
        {
          SellerID = joined.SalesPerson.BusinessEntityId,
          Seller = p.FirstName + " " + (p.MiddleName ?? "") + " " + p.LastName,
          joined.SalesPerson.SalesLastYear,
          joined.SalesPerson.SalesQuota,
          OrdersCount = 1,
          TotalSales = joined.SalesOrderHeader.TotalDue,
          MaxOrderSale = joined.SalesOrderHeader.TotalDue,
          MinOrderSale = joined.SalesOrderHeader.TotalDue,
          SalesTerritory = joined.SalesTerritory.Name
        })
    .GroupBy(joined => new
    {
      joined.SellerID,
      joined.Seller,
      joined.SalesLastYear,
      joined.SalesQuota,
      joined.SalesTerritory
    })
    .Select(grouped => new SalesOverview
    {
      SellerID = grouped.Key.SellerID,
      Seller = grouped.Key.Seller,
      SalesLastYear = grouped.Key.SalesLastYear,
      SalesQuota = grouped.Key.SalesQuota,
      OrdersCount = grouped.Count(),
      TotalSales = grouped.Sum(x => x.TotalSales),
      MaxOrderSale = grouped.Max(x => x.MaxOrderSale),
      MinOrderSale = grouped.Min(x => x.MinOrderSale),
      SalesTerritory = grouped.Key.SalesTerritory
    })
    .OrderByDescending(x => x.TotalSales)
    .ToList();

      if (result == null || result.Count == 0)
      {
        return NotFound();
      }

      return result;

    }

    [HttpGet]
    [Route("/{personName}/{year}")]
    public ActionResult<IEnumerable<SalesByPersonAndYear>> GetByPersonNameAndYear(string personName, int year)
    {

      var result = _context.SalesPeople
    .Join(
        _context.SalesOrderHeaders,
        sp => sp.BusinessEntityId,
        soh => soh.SalesPersonId,
        (sp, soh) => new { SalesPerson = sp, SalesOrderHeader = soh })
    .Join(
        _context.SalesTerritories,
        joined => joined.SalesPerson.TerritoryId,
        st => st.TerritoryId,
        (joined, st) => new { joined.SalesPerson, joined.SalesOrderHeader, SalesTerritory = st })
    .Join(
        _context.Employees,
        joined => joined.SalesOrderHeader.SalesPersonId,
        e => e.BusinessEntityId,
        (joined, e) => new { joined.SalesPerson, joined.SalesOrderHeader, joined.SalesTerritory, Employee = e })
    .Join(
        _context.Persons,
        joined => joined.SalesPerson.BusinessEntityId,
        p => p.BusinessEntityId,
        (joined, p) => new
        {
          joined.SalesPerson.BusinessEntityId,
          FullName = p.FirstName + " " + (p.MiddleName ?? "") + " " + p.LastName,
          joined.Employee.JobTitle,
          joined.SalesTerritory.Name,
          TotalSales = joined.SalesOrderHeader.SubTotal,
          Year = joined.SalesOrderHeader.OrderDate.Year
        })
    .Where(joined => EF.Functions.Like(joined.FullName, $"%{personName}%") && joined.Year == year)
    .GroupBy(joined => new
    {
      joined.BusinessEntityId,
      joined.FullName,
      joined.JobTitle,
      joined.Name,
      joined.Year
    })
    .Select(grouped => new SalesByPersonAndYear
    {
      SalesPersonId = grouped.Key.BusinessEntityId,
      FullName = grouped.Key.FullName,
      JobTitle = grouped.Key.JobTitle,
      SalesTerritory = grouped.Key.Name,
      TotalSales = grouped.Sum(x => x.TotalSales),
      Year = grouped.Key.Year
    })
    .ToList();


      if (result == null || result.Count == 0)
      {
        return NotFound();
      }

      return result;
    }

  }
}