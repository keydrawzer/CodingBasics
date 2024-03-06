using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll();
        if (products == null || products.Count == 0)
        {
            return NotFound("No products found.");
        }
        return Ok(products);
    }

    [HttpGet("name/{name}")]
    public IActionResult GetProductsByName(string name)
    {
        var products = _productService.GetProductsByName(name);
        if (products == null || products.Count == 0)
        {
            return NotFound($"No products found with the name '{name}'.");
        }
        return Ok(products);
    }

    [HttpGet("category/{categoryType}")]
    public IActionResult GetProductByCategoryType(string categoryType)
    {
        var products = _productService.GetProductByCategoryType(categoryType);
        if (products == null || products.Count == 0)
        {
            return NotFound($"No products found in the category '{categoryType}'.");
        }
        return Ok(products);
    }

    [HttpGet("name-and-category")]
    public IActionResult GetProductByNameAndCategoryType([FromQuery] string name, [FromQuery] string categoryType)
    {
        var products = _productService.GetProductByNameAndCategoryType(name, categoryType);
        if (products == null || products.Count == 0)
        {
            return NotFound($"No products found with the name '{name}' in the category '{categoryType}'.");
        }
        return Ok(products);
    }
}
