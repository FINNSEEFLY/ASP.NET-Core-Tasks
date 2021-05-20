using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Core_Web_API__Task_1_.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_Core_Web_API__Task_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        // GET: api/<LocationController>/coord?1,2,3
        [HttpGet]
        public IActionResult Get([FromQuery][ModelBinder(Name = "coord")] Point point)
        {
            return new JsonResult(point);
        }


    }
}
