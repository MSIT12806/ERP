using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_NET6.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }
    }
}
