using Domain_Train;
using NSubstitute;
using Train;
using static Domain_Train.StationData;

namespace DomainTest
{
    public class BuyTicketTests
    {
        ITrainPersistant TrainStore;
        TicketOperator ticketOperation;
        [SetUp]
        public void Setup()
        {
            //set empty train
            string trainNo = "219";
            Carbin carbin1 = new Carbin(trainNo, 16, 1);
            Carbin carbin2 = new Carbin(trainNo, 16, 2);
            Carbin carbin3 = new Carbin(trainNo, 16, 3);
            StationInfo taipei = new StationInfo(Taipei, new TimeOnly(6, 0), new TimeOnly(6, 0));
            StationInfo banqiao = new StationInfo(Banqiao, new TimeOnly(6, 15), new TimeOnly(6, 17));
            StationInfo taoyuan = new StationInfo(Taoyuan, new TimeOnly(6, 55), new TimeOnly(6, 57));
            StationInfo taichung = new StationInfo(Taichung, new TimeOnly(7, 44), new TimeOnly(6, 46));
            StationInfo kaohsiung = new StationInfo(Kaohsiung, new TimeOnly(10, 13), new TimeOnly(10, 15));
            var emptyTrain = new TrainData(
                trainNo,
                TrainData.TrainType.自強, false, null,
                new Carbin[] { carbin1, carbin2, carbin3 },
                new StationInfo[] { taipei, banqiao, taoyuan, taichung, kaohsiung }
                );
            TrainStore = NSubstitute.Substitute.For<ITrainPersistant>();
            TrainStore.GetTrain(trainNo).Returns(emptyTrain);
            ticketOperation = new TicketOperator(TrainStore);

            //set full train
        }

        [Test]
        public void PreTest()
        {
            var train = TrainStore.GetTrain("219");
            Assert.AreEqual("219", train.TrainID);
        }
        [Test]
        public void Test1()
        {
            var ticket = ticketOperation.BuyTicket("219", Taipei, Taichung, 1, 1);
            Assert.AreEqual(Taipei, ticket.StartStation);
            Assert.AreEqual(Taichung, ticket.TargetStation);
        }
    }
}