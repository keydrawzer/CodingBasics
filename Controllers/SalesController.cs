namespace CodingBasics.Controllers;
using AutoMapper;
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
    public IActionResult GetSalesWithSalesPerson()
    {
        var sales = _salesService.GetSalesWithSalesPerson();
        if (sales == null || sales.Count == 0){
             return NotFound("No sales found.");
        }

    
        return Ok(sales);
    }
}
