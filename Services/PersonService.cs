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
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public List<VEmployee> GetPersonByNameAndPersonType(string name, string personType)
        {
            try
            {
                var query = "SELECT * " +
                    $"FROM [AdventureWorks2022].[HumanResources].[vEmployee] A " +
                    $"INNER JOIN Person.Person B ON A.BusinessEntityID = B.BusinessEntityID " +
                    $"WHERE " +
                    $"    ('{name}' ='' OR '{name}' IS NULL OR CONCAT(A.FirstName, ' ', A.MiddleName, ' ', A.LastName) LIKE '%{name}%') " +
                    $"    AND " +
                    $"    ('{personType}' = '' OR '{personType}' IS NULL OR B.PersonType = '{personType}')";

                    
                var results = _dbContext.VEmployees
                    .FromSqlRaw(query)
                    .ToList();

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
