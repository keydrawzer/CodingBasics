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
}