using CodingBasics.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingBasics.Controllers
{

    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello World!!!");
        }
    }

}

