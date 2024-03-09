using CodingBasics.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingBasics.Controllers
{
    
    [ApiController]
    [Route("/test")]
    public class TestController : ControllerBase
    {

        private readonly AdventureWorks2022Context _context;

        public TestController(AdventureWorks2022Context context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult TestDatabaseConnection()
        {
            try
            {
                // Attempt to connect to the database by querying a table
                _context.Database.CanConnect();

                // If the connection is successful, return OK status
                return Ok("Database connection successful");
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a 500 Internal Server Error with the exception message
                return StatusCode(500, $"Database connection failed: {ex.Message}");
            }
        }
    }
}