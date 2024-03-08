using CodingBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingBasics.Services
{
    public class ProductService
    {
        private readonly AdventureWorksDbContext _dbContext;

        public ProductService(AdventureWorksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<VProductAndDescription> GetAllProducts()
        {
            return _dbContext.VProductAndDescriptions
                .OrderBy(p => p.ProductId)
                .ToList();
        }

        public List<VProductAndDescription> GetProductsByName(string name)
        {
            return _dbContext.VProductAndDescriptions
                .Where(p => p.Name.Contains(name))
                .OrderBy(p => p.ProductId)
                .ToList();
        }

        public List<VProductAndDescription> GetProductByCategoryType(string categoryType)
        {
            try
            {
                var query = "SELECT " +
                    "a.ProductID, a.Name, d.Name as ProductModel, e.CultureID , f.Description " +
                    "FROM [AdventureWorks2022].[Production].[Product] a " +
                    "INNER JOIN [AdventureWorks2022].[Production].[ProductSubcategory]  b ON a.ProductSubcategoryID = b.ProductSubcategoryID " +
                    "INNER JOIN [AdventureWorks2022].[Production].[ProductCategory] c ON b.ProductCategoryID = c.ProductCategoryID "+
                    "INNER JOIN [AdventureWorks2022].[Production].[ProductModel] d ON a.ProductModelID = d.ProductModelID " +
                    "INNER JOIN [AdventureWorks2022].[Production].[ProductModelProductDescriptionCulture] e ON d.ProductModelID = e.ProductModelID " +
                    "INNER JOIN [AdventureWorks2022].[Production].[ProductDescription] f ON f.ProductDescriptionID = e.ProductDescriptionID " +
                    $"WHERE c.Name LIKE '%{categoryType}%'";
            
                var results = _dbContext.VProductAndDescriptions
                    .FromSqlRaw(query)
                    .ToList();

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public List<VProductAndDescription>? GetProductByNameAndCategoryType(string name, string categoryType)
        {
        try{
            var query =
                "SELECT " +
                "a.ProductID, a.Name, d.Name as ProductModel, e.CultureID , f.Description " +
                "FROM [AdventureWorks2022].[Production].[Product] a " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductSubcategory]  b ON a.ProductSubcategoryID = b.ProductSubcategoryID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductCategory] c ON b.ProductCategoryID = c.ProductCategoryID "+
                "INNER JOIN [AdventureWorks2022].[Production].[ProductModel] d ON a.ProductModelID = d.ProductModelID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductModelProductDescriptionCulture] e ON d.ProductModelID = e.ProductModelID " +
                "INNER JOIN [AdventureWorks2022].[Production].[ProductDescription] f ON f.ProductDescriptionID = e.ProductDescriptionID " +
                $"WHERE a.Name LIKE '%{name}%' AND c.Name LIKE '%{categoryType}%' AND e.CultureID ='en'";

                var results = _dbContext.VProductAndDescriptions
                    .FromSqlRaw(query)
                    .ToList();

                return results;
            
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
        }

    }
}