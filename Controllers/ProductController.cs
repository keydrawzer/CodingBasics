using Microsoft.AspNetCore.Mvc;
using CodingBasics.Models;
using CodingBasics.Services;

namespace CodingBasics.Controllers
{
    [ApiController] // Annotation for handing minimal APIS request
    [Route("[controller]")] // Maps the controller route to its given name (Product Controller)

      public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("GetByName")]
        public ActionResult<IEnumerable<Product>> GetByName(string name)
        {
            var products = _productService.GetProductByName(name);
            return Ok(products);
        }

        [HttpGet("GetByCategoryType")]
        public ActionResult<IEnumerable<Product>> GetByCategoryType(string category)
        {
            var products = _productService.GetProductByCategoryType(category);
            return Ok(products);
        }
    }
}