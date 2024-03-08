using Microsoft.EntityFrameworkCore;
using CodingBasics.Features.Sales.Domain.Entities;
using CodingBasics.Features.Sales.Infrastructure.Repositories;

namespace CodingBasics.Features.Sales.Application.Services
{
    public interface ISalesOrderHeaderService
    {
        Task<List<SalesOrderHeader>?> GetAll();
    }

    public class SalesOrderHeaderService : ISalesOrderHeaderService
    {
        private readonly ISalesOrderHeaderRepository _repository;

        public SalesOrderHeaderService(ISalesOrderHeaderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SalesOrderHeader>?> GetAll()
        {
            return await _repository.GetAllSalesOrderHeaders();
        }
    }
}
