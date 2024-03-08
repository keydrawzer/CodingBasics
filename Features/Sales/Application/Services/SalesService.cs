using Microsoft.EntityFrameworkCore;
using CodingBasics.Features.Sales.Domain.Entities;
using CodingBasics.Features.Sales.Infrastructure.Repositories;

namespace CodingBasics.Features.Sales.Application.Services
{
    public interface ISalesService
    {
        Task<List<SalesOrderHeader>?> GetAll();
        Task<List<SalesOrderHeader>?> GetSalesByPersonAndYear(string name, int year);
        Task<List<string>?> GetSalesPersonNames();
    }

    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _repository;

        public SalesService(ISalesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SalesOrderHeader>?> GetAll()
        {
            return await _repository.GetAllSalesOrderHeaders();
        }

        public async Task<List<SalesOrderHeader>?> GetSalesByPersonAndYear(string name, int year)
        {
            return await _repository.FilterSalesByPersonAndYear(name, year);
        }

        public async Task<List<string>?> GetSalesPersonNames()
        {
            return await _repository.GetSalesPeopleNames();
        }
    }
}
