using Microsoft.AspNetCore.Mvc;
using CodingBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly AdventureWorks2022Context _context;

    public ProductController(AdventureWorks2022Context context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("")]
    public ActionResult<IEnumerable<VProductAndDescription>> GetAll()
    {
      return _context.VProductAndDescriptions.ToList();

    }

    [HttpGet]
    [Route("name/{productName}")]
    public ActionResult<IEnumerable<VProductAndDescription>> GetByName(string productName)
    {

      var products = _context.VProductAndDescriptions.Where(product => EF.Functions.Like(product.Name, $"%{productName}%")).ToList();

      if (products == null || products.Count == 0)
      {
        return NotFound();
      }

      return products;
    }

    [HttpGet]
    [Route("category/{categoryType}")]
    public ActionResult<IEnumerable<VProductAndDescription>> GetByCategoryType(string categoryType)
    {
      var products = _context.Products
    .Join(
        _context.ProductSubcategories,
        product => product.ProductSubcategoryId,
        subcategory => subcategory.ProductSubcategoryId,
        (product, subcategory) => new { Product = product, Subcategory = subcategory })
    .Join(
        _context.ProductCategories,
        joined => joined.Subcategory.ProductCategoryId,
        category => category.ProductCategoryId,
        (joined, category) => new { joined.Product, joined.Subcategory, Category = category })
    .Join(
        _context.ProductModels,
        joined => joined.Product.ProductModelId,
        model => model.ProductModelId,
        (joined, model) => new { joined.Product, joined.Subcategory, joined.Category, Model = model })
    .Join(
        _context.ProductModelProductDescriptionCultures,
        joined => joined.Model.ProductModelId,
        culture => culture.ProductModelId,
        (joined, culture) => new { joined.Product, joined.Subcategory, joined.Category, joined.Model, Culture = culture })
    .Join(
        _context.ProductDescriptions,
        joined => joined.Culture.ProductDescriptionId,
        description => description.ProductDescriptionId,
        (joined, description) => new
        {
          joined.Product.ProductId,
          joined.Product.Name,
          joined.Category,
          ProductModel = joined.Model.Name,
          joined.Culture.CultureId,
          Description = description.Description
        })
    .Where(result => result.Category.Name.Contains(categoryType))
    .Select(p => new VProductAndDescription
    {
      ProductId = p.ProductId,
      Name = p.Name,
      ProductModel = p.ProductModel,
      CultureId = p.CultureId,
      Description = p.Description
    })
    .ToList();

      return products;
    }


  }
}