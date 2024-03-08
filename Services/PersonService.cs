using CodingBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingBasics.Services
{
    public class PersonService
    {
        private readonly AdventureWorksDbContext _dbContext;

        public PersonService(AdventureWorksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<VEmployee> GetAllPersons()
        {
            return _dbContext.VEmployees
                .OrderBy(e => e.BusinessEntityId)
                .ToList();
        }

        public List<VEmployee> GetPersonsByName(string name)
        {
            try
            {
                var persons = _dbContext.VEmployees
                    .AsEnumerable()
                    .Where(e => $"{e.FirstName} {e.MiddleName} {e.LastName}".Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return persons;
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public List<VEmployee> GetPersonsByEmpType(string personType)
        {
            try
            {
                var results = _dbContext.VEmployees
                    .FromSqlRaw($"SELECT * FROM HumanResources.vEmployee WHERE BusinessEntityID IN (SELECT BusinessEntityID FROM Person.Person WHERE PersonType = '{personType}')")
                    .ToList();

                return results;
            }
            catch (Exception ex)
            {
                // Log the error or handle it appropriately
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
