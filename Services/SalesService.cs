using CodingBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingBasics.Services
{
    public class SalesService
    {
        private readonly AdventureWorksDbContext _dbContext;

        public SalesService(AdventureWorksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TotalSales> GetTotalSales(){
        try
        {
            var query = "SELECT " +
                "sp.BusinessEntityID AS SalesPersonId, CONCAT(p.FirstName, ' ', p.LastName) AS PersonName, SUM(soh.TotalDue) AS Total " +
                "FROM [AdventureWorks2022].[Sales].[SalesPerson] sp " +
                "INNER JOIN [AdventureWorks2022].[Person].[Person]  p ON sp.BusinessEntityID = p.BusinessEntityID " +
                "INNER JOIN [AdventureWorks2022].[Sales].[SalesOrderHeader] soh ON sp.BusinessEntityID = soh.SalesPersonID " +
                "GROUP BY sp.BusinessEntityID, p.FirstName, p.LastName " +
                "ORDER BY sp.BusinessEntityID ASC";
            
            var results = _dbContext.TotalSales
                .FromSqlRaw(query)
                .ToList();

            return results;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }


    public List<SalesByYear>? GetSalestByNameAndYear(string name, string year)
        {
        try{
            var query = "SELECT " +
            "sp.BusinessEntityID AS SalesPersonId, " +
            "CONCAT(p.FirstName, ' ', p.LastName) AS Name, " +
            "YEAR(soh.OrderDate) AS Year, " +
            "soh.SalesOrderID, " +
            "soh.TotalDue " +
            "FROM " +
            "[Sales].[SalesPerson] sp " +
            "INNER JOIN [Person].[Person] p ON sp.BusinessEntityID = p.BusinessEntityID " +
            "INNER JOIN [Sales].[SalesOrderHeader] soh ON sp.BusinessEntityID = soh.SalesPersonID " +
            "WHERE " +
            $"(CONCAT(p.FirstName, P. MiddleName, p.LastName) LIKE '%{name}%' OR " +
            $"CONCAT(p. FirstName, ' ', P.LastName) LIKE '%{name}%') " +
            $"AND (YEAR(soh.OrderDate) = '{year}')";

                var results = _dbContext.SalesByYears
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