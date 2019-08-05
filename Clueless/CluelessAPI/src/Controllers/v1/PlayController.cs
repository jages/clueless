using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cluelessapi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class PlayController : Controller
    {
        
        [HttpPost]
        public IActionResult Post([FromBody] string location)
        {

            Console.WriteLine("in post controller, also " + location);
                return Ok(location);

        }
        
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("in get controller");
            return Ok();

        }
        
        
    }
}