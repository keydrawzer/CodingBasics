using CodingBasics.Models;
using CodingBasics.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]


public class PersonController : ControllerBase
{
    private readonly PersonService _personService;

    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAllPersons()
    {
        var persons = _personService.GetAllPersons().ToList();
        return Ok(persons);
    }

    [HttpGet("name/{name}")]
    public IActionResult GetPersonsByName(string name)
    {
        var persons = _personService.GetPersonsByName(name);
        return Ok(persons);
    }

    [HttpGet("type/{type}")]
    public IActionResult GetPersonsType(string type)
    {
        var persons = _personService.GetPersonsByType(type);
        return Ok(persons);
    }

    [HttpGet("filter")]
    public IActionResult GetPersonsByNameAndType([FromQuery] string name, [FromQuery] string type)
    {
        var persons = _personService.GetPersonsByNameAndType(name, type);
        return Ok(persons);
    }

}
