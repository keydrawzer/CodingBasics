using Microsoft.AspNetCore.Mvc;
using CodingBasics.Models;

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
    public ActionResult<IEnumerable<ProductData>> GetAll()
    {
      return GetProductQueryData();

    }

    [HttpGet]
    [Route("name/{name}")]
    public ActionResult<IEnumerable<ProductData>> GetByName(string name)
    {

      var products = GetProductQueryData().Where(product => product.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();

      return products;
    }

    [HttpGet]
    [Route("category/{category}")]
    public ActionResult<IEnumerable<ProductData>> GetByCategory(string category)
    {
      var products = GetProductQueryData()
    .Where(product => product.Category.Contains(category, StringComparison.CurrentCultureIgnoreCase))
    .ToList();

      return products;
    }

    [HttpGet]
    [Route("name/{name}/category/{category}")]
    public ActionResult<IEnumerable<ProductData>> GetByNameAndCategory(string name, string category)
    {
      var products = GetProductQueryData()
    .Where(product => product.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase) && product.Category.Contains(category, StringComparison.CurrentCultureIgnoreCase))
    .ToList();

      return products;
    }

    private List<ProductData> GetProductQueryData()
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
        (joined, description) => new ProductData
        {
          ProductId = joined.Product.ProductId,
          Name = joined.Product.Name,
          Category = joined.Category.Name,
          ProductModel = joined.Model.Name,
          CultureId = joined.Culture.CultureId,
          Description = description.Description
        })
    .ToList();

      return products;
    }


  }
}