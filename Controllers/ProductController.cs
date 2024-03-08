using CodingBasics.Models;
using CodingBasics.Services;
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
    public IActionResult GetAllProducts()
    {
        var products = _productService.GetAllProducts().ToList();
        return Ok(products);
    }

    [HttpGet("name/{name}")]
    public IActionResult GetProductsByName(string name)
    {
        var products = _productService.GetProductByName(name);
        return Ok(products);
    }


    [HttpGet("subcategory/{subcategoryName}")]
    public ActionResult<List<Product>> GetProductsBySubcategoryName(string subcategoryName)
    {
        var products = _productService.GetProductsBySubcategoryName(subcategoryName);
        if (products == null || products.Count == 0)
        {
            return NotFound();
        }
        return Ok(products);
    }



    /*
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
    */
}
