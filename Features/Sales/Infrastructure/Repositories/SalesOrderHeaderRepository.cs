﻿using CodingBasics.Features.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Features.Sales.Infrastructure.Repositories;

public interface ISalesOrderHeaderRepository
{
    Task<List<SalesOrderHeader>?> GetAllSalesOrderHeaders();
    Task<List<SalesOrderHeader>?> FilterSalesByPersonAndYear(string personName, int year);
}

public class SalesOrderHeaderRepository(CodingBasicsDbContext dbContext) : ISalesOrderHeaderRepository
{
    private readonly CodingBasicsDbContext _dbContext = dbContext;

    public async Task<List<SalesOrderHeader>?> GetAllSalesOrderHeaders()
    {
        try
        {
            return await _dbContext.SalesOrderHeaders.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public async Task<List<SalesOrderHeader>?> FilterSalesByPersonAndYear(string personName, int year)
    {
        try
        {
            var result = await _dbContext.VSalesPeople
                .Where(x => (x.FirstName + " " + x.MiddleName + " " + x.LastName) == personName)
                .Join(
                    _dbContext.SalesOrderHeaders,
                    a => a.BusinessEntityId,
                    b => b.SalesPersonId,
                    (a, b) => new { Person = a, Sale = b }
                )
                .Where(x => x.Sale.OrderDate.Year == year)
                .Select(joinResult => joinResult.Sale)
                .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }
}
