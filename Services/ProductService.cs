using CodingBasics.Data;
using CodingBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Services
{
    public class ProductService
    {
        private readonly BasicDbContext _context;

        public ProductService(BasicDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return _context.Products.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JustError: {ex.Message}");
                return null;
            }
        }

        public List<Product> GetProductByName(string name)
        {
            try
            {
                return _context.Products
                    .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JustError: {ex.Message}");
                return null;
            }
        }

        public List<Product> GetProductByCategoryType(string category)
        {
            try
            {
                return _context.Products
                    .Join(_context.ProductSubcategories,
                        product => product.ProductSubcategoryID,
                        subcategory => subcategory.ProductSubcategoryID,
                        (product, subcategory) => new { product, subcategory })
                    .Join(_context.ProductCategories,
                        combined => combined.subcategory.ProductCategoryID,
                        category => category.ProductCategoryID,
                        (combined, category) => new { combined.product, combined.subcategory, category })
                    .Where(p => p.category.Name == category)
                    .Select(p => p.product)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JustError: {ex.Message}");
                return null;
            }
        }
    }

}
