using Microsoft.AspNetCore.Mvc;

namespace CodingBasics.Controllers;

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
    public IActionResult GetAll()
    {
        var people = _personService.GetAll();
        if (people == null || people.Count == 0)
        {
            return NotFound("No people found.");
        }
        return Ok(people);
    }

    [HttpGet("{name}")]
    public IActionResult GetPersonByName(string name)
    {
        var people = _personService.GetPersonByName(name);
        if (people == null || people.Count == 0)
        {
            return NotFound($"No people found with the name '{name}'.");
        }
        return Ok(people);
    }

     [HttpGet("{type}")]
    public IActionResult GetPersonType(string type)
    {
        var people = _personService.GetPersonByPersonType(type);
        if (people == null || people.Count == 0)
        {
            return NotFound($"No people found with the type '{type}'.");
        }
        return Ok(people);
    }

        [HttpGet("{name}/{personType}")]
    public IActionResult GetPersonByNameAndPersonType(string name, string personType)
    {
        var people = _personService.GetPersonByNameAndPersonType(name, personType);
        if (people == null || people.Count == 0)
        {
            return NotFound($"No people found with the name '{name}' and person type '{personType}'.");
        }
        return Ok(people);
    }
}