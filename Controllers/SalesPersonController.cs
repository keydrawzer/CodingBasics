using CodingBasics.Models;
using CodingBasics.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CodingBasics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesPersonController : ControllerBase
    {
        private readonly SalesPersonService _salesPersonService;

        public SalesPersonController(SalesPersonService salesPersonService)
        {
            _salesPersonService = salesPersonService;
        }

        // This action method retrieves a summary of all salespersons
        [HttpGet]
        public ActionResult<IEnumerable<SalesPerson>> GetSalesSummary()
        {
            var salesSummary = _salesPersonService.GetSalesSummary();
            return Ok(salesSummary);
        }

        // This action method retrieves sales data for a specific salesperson in a particular year based on name and year provided
        [HttpGet("GetForSalesPersonAndYear")]
        public ActionResult<IEnumerable<SalesPerson>> GetForSalesPersonAndYear(string name, int year){
            var salesSummary = _salesPersonService.GetSalesForSalesPersonAndYear(name, year);
            return Ok(salesSummary);
        }
    }
}