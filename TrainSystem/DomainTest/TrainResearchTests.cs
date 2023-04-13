                                                                                                                                                using Domain_Train;
using Domain_Train.dev;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Train;
using static Domain_Train.StationDatas;

namespace DomainTest
{
    public class TrainResearchTests
    {
        ITrainPersistant TrainStore;
        TicketOperator ticketOperation;
        IStationPersistant StationStore;
        //DbContextOptions<TrainEF> Options;
        [SetUp]
        public void Setup()
        {
            //EF 毛一堆...
            //Options = new DbContextOptionsBuilder<TrainEF>().UseInMemoryDatabase("TestDatabase").Options;
            //TrainStore = new TrainEF(Options);
            StationStore = new FakeStationDB();
            TrainStore = new FakeTrainDb();
            FakeDataFunction.InjectTestData(TrainStore);
            //add train 219
            string trainNo = "219";
            var k = Domain_Train.Station.TrunkLine.環島線逆;
            Carbin carbin1 = new Carbin(trainNo, 16, 1);
            Carbin carbin2 = new Carbin(trainNo, 16, 2);
            Carbin carbin3 = new Carbin(trainNo, 16, 3);
            StationInfo taipei = new StationInfo(Taipei.StationName, StationStore.GetStationNo(Taipei.StationName, k), new TimeOnly(6, 0), new TimeOnly(6, 0));
            StationInfo banqiao = new StationInfo(Banqiao.StationName, StationStore.GetStationNo(Banqiao.StationName, k), new TimeOnly(6, 15), new TimeOnly(6, 17));
            StationInfo taoyuan = new StationInfo(Taoyuan.StationName, StationStore.GetStationNo(Taoyuan.StationName, k), new TimeOnly(6, 55), new TimeOnly(6, 57));
            StationInfo taichung = new StationInfo(Taichung.StationName, StationStore.GetStationNo(Taichung.StationName, k), new TimeOnly(7, 44), new TimeOnly(6, 46));
            StationInfo kaohsiung = new StationInfo(Kaohsiung.StationName, StationStore.GetStationNo(Kaohsiung.StationName, k), new TimeOnly(10, 13), new TimeOnly(10, 15));
            var Train = new TrainData(
                trainNo,
                 k,
                TrainData.TrainType.自強, false, null,
                new Carbin[] { carbin1, carbin2, carbin3 },
                new StationInfo[] { taipei, banqiao, taoyuan, taichung, kaohsiung }
                );
            TrainStore.AddTrain(Train);
            ticketOperation = new TicketOperator(TrainStore);
        }

        [Test]
        public void PreTest()
        {
            var train = TrainStore.GetTrain("219");
            Assert.AreEqual("219", train.TrainID);
        }

        [Test]
        public void GetTrainsByIDTest()
        {
            //arrange: 

            //act
            var trains = TrainStore.GetTrainsByID("219", new DateOnly(2023, 4, 9));

            //assert
            Assert.AreEqual(1, trains.Count());
        }

        [Test]
        public void GetTrainsByIDTest_IsNotRunDate()
        {
            //arrange: 
            var train219 = TrainStore.GetTrain("219");
            train219.AddNoRunDate(new DateOnly(2023, 4, 9));
            TrainStore.EditTrain(train219);

            //act
            var trains = TrainStore.GetTrainsByID("219", new DateOnly(2023, 4, 9));

            //assert
            Assert.AreEqual(0, trains.Count());
        }
        [Test]
        public void GetTrainsByTime()
        {
            //arrange
            string startStation = "taipei";
            string targetStation = "banqiao";
            DateOnly date = new DateOnly(2023, 4, 9);
            TimeOnly startTime = new TimeOnly(6, 0);
            TimeOnly endTime = new TimeOnly(12, 0);
            char searchType = 'S';
            //act
            var trains = TrainStore.GetTrainsByTime(startStation, targetStation, date, searchType, startTime, endTime);
            //assert
            Assert.AreEqual(2, trains.Count());
            Assert.IsNotNull(trains.FirstOrDefault(i => i.TrainID == "219"));
            Assert.IsNotNull(trains.FirstOrDefault(i => i.TrainID == "511"));
        }
        [Test]
        public void GetTrainsByStation()
        {
            //arrange
            string station = "taichung";
            DateOnly date = new DateOnly(2023, 4, 9);
            //act
            var trains = TrainStore.GetTrainsByStation(station, date);
            //assert
            Assert.AreEqual(3, trains.Count());
            Assert.IsNotNull(trains.FirstOrDefault(i => i.TrainID == "219"));
            Assert.IsNotNull(trains.FirstOrDefault(i => i.TrainID == "2193"));
            Assert.IsNotNull(trains.FirstOrDefault(i => i.TrainID == "273"));
        }
    }
}