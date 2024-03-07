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
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return _context.Persons.Take(20).ToList();
        }

        [HttpGet]
        [Route("name/{name}")]
        public ActionResult<IEnumerable<Person>> GetByName(string name)
        {

            var persons = _context.Persons.Where(person => EF.Functions.Like(person.FirstName, name) || EF.Functions.Like(person.MiddleName, name) || EF.Functions.Like(person.LastName, name)).ToList();

            if (persons == null || persons.Count == 0)
            {
                return NotFound();
            }
            return persons;
        }

        [HttpGet]
        [Route("type/{personType}")]
        public ActionResult<IEnumerable<Person>> GetByPersonType(string personType)
        {

            var persons = _context.Persons.Where(person => person.PersonType == personType).ToList();

            if (persons == null || persons.Count == 0)
            {
                return NotFound();
            }
            return persons;
        }

        [HttpGet]
        [Route("name/{personName}/type/{personType}")]
        public ActionResult<IEnumerable<Person>> GetByPersonNameAndType(string personName, string personType)
        {

            var persons = _context.Persons.Where(person => (EF.Functions.Like(person.FirstName, personName) || EF.Functions.Like(person.MiddleName, personName) || EF.Functions.Like(person.LastName, personName)) && person.PersonType == personType).ToList();

            if (persons == null || persons.Count == 0)
            {
                return NotFound();
            }
            return persons;
        }

    }
}