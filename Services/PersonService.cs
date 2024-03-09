using CodingBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Services
{
    public class PersonService 
    {
        private readonly CodingBasicsContext _context;

        public PersonService ( CodingBasicsContext context)
        {
            _context = context;
        }
        public List<Employee> GetAllPersons() 
        {
            try
            {
                return _context.Employees.ToList(); //Retrieve all employees from the database
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Sorry, it looks like there is an error:{ex.Message}");
                return null; 
            }
        }

        public List<Employee> GetPersonByName(string name)
        {
            try 
            {
                return _context.Employees
                        .Where(e => EF.Functions.Like(e.FirstName + " " + e.MiddleName + " " + e.LastName, $"%{name}%"))
                        .ToList(); //Find employees with a name matching the given name
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Sorry, it looks like there is an error:{ex.Message}");
                return null;
            }
        }

        public List<Employee> GetPersonByPersonType(string personType)
        {
            try
            {
                return _context.Employees // Join Employees table with Persons table to access PersonType
                    .Join(_context.Persons, 
                        Employee => Employee.BusinessEntityID,
                        person => person.BusinessEntityID,
                        (Employee, person) => new { Employee, person }) //Projection to work with both objects
                    .Where( e => e.person.PersonType == personType)
                    .Select( e => e.Employee)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Sorry, it looks like there is an error:{ex.Message}");
                return null; 
            }
        }

        public List<Employee> GetPersonByNameAndPersonType(string name = null, string personType = null)
        {
            try
            {
                // Build a query with optional filtering for both name and personType
                return _context.Employees
                .Join(_context.Persons,
                        Employee => Employee.BusinessEntityID,
                        person => person.BusinessEntityID, 
                        (Employee, person) => new { Employee, person })
                .Where( e => 
                    (string.IsNullOrEmpty(name) || EF.Functions.Like(e.Employee.FirstName + " " + e.Employee.MiddleName + " " + e.Employee.LastName, $"%{name}%")) &&
                    (string.IsNullOrEmpty(personType) || e.person.PersonType == personType))
                .Select( e => e.Employee)
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Sorry, it looks like there is an error:{ex.Message}");
                return null;
            }
        }
    }
}