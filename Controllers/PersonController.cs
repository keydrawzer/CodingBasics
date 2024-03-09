using Microsoft.AspNetCore.Mvc;
using CodingBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingBasics.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AdventureWorks2022Context _context;

        public PersonController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<VEmployee>> GetAll()
        {
            return _context.VEmployees.ToList();

        }

        [HttpGet]
        [Route("name/{name}")]
        public ActionResult<IEnumerable<VEmployee>> GetByName(string name)
        {

            var persons = _context.VEmployees.Where(person => EF.Functions.Like(person.FirstName, name) || EF.Functions.Like(person.MiddleName, name) || EF.Functions.Like(person.LastName, name)).ToList();

            return persons;
        }

        [HttpGet]
        [Route("type/{personType}")]
        public ActionResult<IEnumerable<VEmployee>> GetByPersonType(string personType)
        {

            var persons = _context.VEmployees
            .Join(
            _context.Persons,
            employee => employee.BusinessEntityId,
            person => person.BusinessEntityId,
            (employee, person) => new { Employee = employee, Person = person })
            .Where(joined => joined.Person.PersonType == personType)
            .Select(joined => joined.Employee)
            .ToList();

            return persons;
        }

        [HttpGet]
        [Route("name/{personName}/type/{personType}")]
        public ActionResult<IEnumerable<VEmployee>> GetByPersonNameAndType(string personName, string personType)
        {

            var persons = _context.VEmployees
            .Join(
            _context.Persons,
            employee => employee.BusinessEntityId,
            person => person.BusinessEntityId,
            (employee, person) => new { Employee = employee, Person = person })
            .Where(joined => joined.Person.PersonType == personType && (joined.Person.FirstName == personName || joined.Person.LastName == personName || joined.Person.MiddleName == personName))
            .Select(joined => joined.Employee)
            .ToList();

            return persons;
        }

    }
}