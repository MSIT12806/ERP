using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        DbContextOptions<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>()
    .UseInMemoryDatabase(databaseName: "MyDb")
    .Options;
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //HttpContext.Session.SetString("Identity", "hello");
            using (var context = new MyDbContext(options))
            {
                var q = context.ApplicationUsers.ToList();
                context.Add(new ApplicationUser());
                context.SaveChanges();
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}