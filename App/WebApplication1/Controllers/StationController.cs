using App_NET6.ViewModel;
using Domain_Train;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NET6MiddleWareStudy;
using Train;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IStationPersistant stationDB;

        public StationController(IStationPersistant db)
        {
            stationDB = db;
        }
        [HttpGet]
        public IEnumerable<StationVM> Get(Station.TrunkLine trunkLine)
        {
            //HttpContext.Session.SetString("Identity", "hello");
            var q = stationDB.GetStations(trunkLine).Select(i=>new StationVM(i, trunkLine));

            return q;
        }
    }
}