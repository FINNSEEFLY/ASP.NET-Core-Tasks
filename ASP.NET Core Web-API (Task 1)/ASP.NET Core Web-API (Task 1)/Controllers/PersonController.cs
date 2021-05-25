using ASP.NET_Core_Web_API__Task_1_.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_Core_Web_API__Task_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET api/<PersonController>/ODJkZjQxZDItYjQxOC00OGFjLThkOGQtZmExZDdmNGUzYTZk
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute][ModelBinder(Name = "id")] Person person)
        {
            return new JsonResult(person);
        }
    }
}