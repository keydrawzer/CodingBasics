using CodingBasics.Features.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Features.Sales.Infrastructure.Repositories;

public interface ISalesOrderHeaderRepository
{
    Task<List<SalesOrderHeader>?> GetAllSalesOrderHeaders();
}

public class SalesOrderHeaderRepository(CodingBasicsDbContext dbContext) : ISalesOrderHeaderRepository
{
    private readonly CodingBasicsDbContext _dbContext = dbContext;

    public async Task<List<SalesOrderHeader>?> GetAllSalesOrderHeaders()
    {
        try
        {
            return await _dbContext.SalesOrdersHeaders.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }
}
