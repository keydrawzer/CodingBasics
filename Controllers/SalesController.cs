namespace CodingBasics.Controllers;
using AutoMapper;
using CodingBasics.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class SalesController : ControllerBase
{
    private readonly SalesService _salesService;


    public SalesController(SalesService salesService)
    {
        _salesService = salesService;

    }

    [HttpGet("sales-per-vendor")]
    public IActionResult GetAllSalesWithPersonnelName()
    {
        try
        {
            var salesData = _salesService.GetSalesSummaryBySalesPerson();
            return Ok(salesData);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



    [HttpGet]
    public IActionResult GetSalesForSalesPerson(string salesPersonName, int year)
    {
        try
        {
            decimal totalSales = _salesService.GetSalesForSalesPerson(salesPersonName, year);
            return Ok(totalSales);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
