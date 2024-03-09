using Microsoft.AspNetCore.Mvc;
using CodingBasics.Models;

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
    [Route("overview")]
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

      return result;

    }

    [HttpGet]
    [Route("{personName}/{year}")]
    public ActionResult<IEnumerable<SalesByPersonAndYear>> GetByPersonNameAndYear(string personName, int year)
    {
      var result = GetSaleQueryData().Where(data => data.FullName.Contains(personName, StringComparison.CurrentCultureIgnoreCase) && DateOnly.Parse(data.Date).Year == year).ToList();

      return result;
    }

    [HttpGet]
    [Route("{personName}")]
    public ActionResult<IEnumerable<SalesByPersonAndYear>> GetByPersonName(string personName)
    {
      var result = GetSaleQueryData().Where(data => data.FullName.Contains(personName, StringComparison.CurrentCultureIgnoreCase)).ToList();

      return result;
    }

    [HttpGet]
    [Route("{year:int}")]
    public ActionResult<IEnumerable<SalesByPersonAndYear>> GetByYear(int year)
    {
      var result = GetSaleQueryData().Where(data => DateOnly.Parse(data.Date).Year == year).ToList();

      return result;
    }


    [HttpGet]
    [Route("")]
    public ActionResult<IEnumerable<SalesByPersonAndYear>> GetAll()
    {
      return GetSaleQueryData();
    }

    private List<SalesByPersonAndYear> GetSaleQueryData()
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
          joined.SalesOrderHeader.SalesOrderId,
          FullName = p.FirstName + " " + (p.MiddleName ?? "") + " " + p.LastName,
          joined.Employee.JobTitle,
          joined.SalesTerritory.Name,
          joined.SalesOrderHeader.SubTotal,
          joined.SalesOrderHeader.OrderDate
        }).GroupBy(joined => new
        {
          joined.SalesOrderId,
          joined.FullName,
          joined.JobTitle,
          joined.Name,
          joined.SubTotal,
          joined.OrderDate
        })
    .Select(grouped => new SalesByPersonAndYear
    {
      SalesOrderId = grouped.Key.SalesOrderId,
      FullName = grouped.Key.FullName,
      JobTitle = grouped.Key.JobTitle,
      SalesTerritory = grouped.Key.Name,
      SubTotal = grouped.Key.SubTotal,
      Date = grouped.Key.OrderDate.ToShortDateString()
    })
    .ToList();

      return result;
    }
  }
}