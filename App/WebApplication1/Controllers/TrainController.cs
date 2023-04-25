using App_NET6.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NET6MiddleWareStudy;
using Train;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ITrainPersistant trainDB;

        public TrainController(ITrainPersistant db)
        {
            trainDB = db;
            FakeDataFunction.InjectTestData(trainDB);
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //HttpContext.Session.SetString("Identity", "hello");
            var q = trainDB.GetTrains();

            return q.Select(i => new WeatherForecast { Date = DateTime.Now, Summary = i.TrainID, TemperatureC = 13 });
        }
    }
}