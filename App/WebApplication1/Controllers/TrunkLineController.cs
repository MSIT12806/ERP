using App_NET6.ViewModel;
using Domain_Train;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NET6MiddleWareStudy;
using Train;
using static Domain_Train.Station;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrunkLineController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IStationPersistant stationDB;

        public TrunkLineController(IStationPersistant db)
        {
            stationDB = db;
        }
        [HttpGet]
        public IEnumerable<TrunkLineVM> Get()
        {
            return Enum.GetValues(typeof(TrunkLine)).Cast<TrunkLine>().Select(i => new TrunkLineVM(i));
        }
    }
}