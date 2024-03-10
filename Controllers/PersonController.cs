using Microsoft.AspNetCore.Mvc;
using CodingBasics.Models;
using CodingBasics.Services;

namespace CodingBasics.Controllers
{
    [ApiController] // Annotation for handing minimal APIS request
    [Route("[controller]")] // Maps the controller route to its given name (PersonController)
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService; 

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            var person = _personService.GetAllPersons();
            return Ok(person);
        }

        [HttpGet("GetByName")]
        public ActionResult<IEnumerable<Person>> GetByName(string name)
        {
            var persons = _personService.GetPersonByName(name);
            return Ok(persons);
        }

        [HttpGet("GetByEmpType")]
        public ActionResult<IEnumerable<Person>> GetByEmpType(string empType)
        {
            var persons = _personService.GetPersonByPersonType(empType);
            return Ok(persons);
        }

        [HttpGet("GetByNameAndType")]
        public ActionResult<IEnumerable<Person>> GetByNameAndType(string name = null, string empType = null)
        {
            var persons = _personService.GetPersonByNameAndPersonType(name, empType);
            return Ok(persons);
        }
    }
}