
using CodingBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Services
{

    public class ProductService
    {

        private readonly AdventureWorks2022Context _context;

        public ProductService(AdventureWorks2022Context context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            return _context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
        }


        public List<Product> GetProductsBySubcategoryName(string subcategoryName)
        {
            return _context.Products
                .Where(p => p.ProductSubcategory.Name == subcategoryName)
                .ToList();
        }
    }
}
