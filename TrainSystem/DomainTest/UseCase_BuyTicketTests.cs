using Domain_Train;
using Domain_Train.dev;
using Domain_Train.interfaces;
using NSubstitute;
using Train;
using static Domain_Train.StationDatas;
using static Train.TicketOperator;

namespace DomainTest
{
    public class UseCase_BuyTicketTests
    {
        ITrainPersistant TrainStore;
        TicketOperator ticketOperation;
        IStationPersistant StationStore;
        IDateTimeProvider dateTimeProvider;
        [SetUp]
        public void Setup()
        {
            dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.Now().Returns(new DateTime(2023, 4, 12, 3, 4, 5));
            MyDateTimeProvider.Ins = dateTimeProvider;

            StationStore = new FakeStationDB();
            //set empty train
            string trainNo = "219";
            var k = Domain_Train.Station.TrunkLine.環島線逆;
            Carbin carbin1 = new Carbin(trainNo, 16, 1);
            Carbin carbin2 = new Carbin(trainNo, 16, 2);
            Carbin carbin3 = new Carbin(trainNo, 16, 3);
            TrainRunToStationInfo taipei = new TrainRunToStationInfo(Taipei.StationName, StationStore.GetStationNo(Taipei.StationName, k), new TimeOnly(6, 0), new TimeOnly(6, 0));
            TrainRunToStationInfo banqiao = new TrainRunToStationInfo(Banqiao.StationName, StationStore.GetStationNo(Banqiao.StationName, k), new TimeOnly(6, 15), new TimeOnly(6, 17));
            TrainRunToStationInfo taoyuan = new TrainRunToStationInfo(Taoyuan.StationName, StationStore.GetStationNo(Taoyuan.StationName, k), new TimeOnly(6, 55), new TimeOnly(6, 57));
            TrainRunToStationInfo taichung = new TrainRunToStationInfo(Taichung.StationName, StationStore.GetStationNo(Taichung.StationName, k), new TimeOnly(7, 44), new TimeOnly(6, 46));
            TrainRunToStationInfo kaohsiung = new TrainRunToStationInfo(Kaohsiung.StationName, StationStore.GetStationNo(Kaohsiung.StationName, k), new TimeOnly(10, 13), new TimeOnly(10, 15));
            var emptyTrain = new TrainData(
                trainNo,
                k,
                TrainData.TrainType.自強, false,
                Tools.Get2023Datas(new TrainRunToStationInfo[] { taipei, banqiao, taoyuan, taichung, kaohsiung }),
                new Carbin[] { carbin1, carbin2, carbin3 }
                );
            TrainStore = NSubstitute.Substitute.For<ITrainPersistant>();
            TrainStore.GetTrain(trainNo).Returns(emptyTrain);
            var trainFinder = new TrainFinder(TrainStore);
            ticketOperation = new TicketOperator(trainFinder);

            //set full train
        }

        [Test]
        public void PreTest()
        {
            var train = TrainStore.GetTrain("219");
            Assert.AreEqual("219", train.TrainID);
        }

        [Test]
        public void BuyTicketTest_WithoutDateTime()
        {
            var ticket = ticketOperation.BuyTicket("219", Taipei.StationName, Taichung.StationName);
            Assert.AreEqual(Taipei.StationName, ticket.StartStation);
            Assert.AreEqual(Taichung.StationName, ticket.TargetStation);
            Assert.AreEqual(DateOnly.FromDateTime(dateTimeProvider.Now()), (ticket.ExpirationDate));
        }

        [Test]
        public void BuyTicketTest()
        {
            var dateAfter3 = dateTimeProvider.Now().AddDays(3);
            var ticket = ticketOperation.BuyTicket("219", Taipei.StationName, Taichung.StationName, DateOnly.FromDateTime(dateAfter3));
            Assert.AreEqual(Taipei.StationName, ticket.StartStation);
            Assert.AreEqual(Taichung.StationName, ticket.TargetStation);
            Assert.AreEqual(DateOnly.FromDateTime(dateAfter3), (ticket.ExpirationDate));
        }
        [Test]
        public void BuyTicketTest_BuyAgainShouldNotInSameCarbin()
        {
            var dateAfter3 = dateTimeProvider.Now().AddDays(3);
            var ticket1 = ticketOperation.BuyTicket("219", Taipei.StationName, Taichung.StationName, DateOnly.FromDateTime(dateAfter3));
            var ticket2 = ticketOperation.BuyTicket("219", Taipei.StationName, Taichung.StationName, DateOnly.FromDateTime(dateAfter3));
            Assert.AreNotEqual(ticket1.Carbin, ticket2.Carbin);
        }

        [Test]
        public void BuyTicketTest_PastTime()
        {
            var dateAfter3 = dateTimeProvider.Now().AddDays(-3);
            Assert.Throws<Notify>(() => ticketOperation.BuyTicket("219", Taipei.StationName, Taichung.StationName, DateOnly.FromDateTime(dateAfter3)));
        }
        [Test]
        public void BuyTicketTest_NoUnsoldSeat()
        {
            for (int i = 0; i < 48; i++)
            {
                ticketOperation.BuyTicket("219", Taipei.StationName, Taichung.StationName);
            }
            Assert.Throws<Notify>(() => ticketOperation.BuyTicket("219", Taipei.StationName, Taichung.StationName));
        }
        [Test]
        public void BuyTicketTest_TwoSeatTogether()
        {
            var tickets = ticketOperation.BuyTickets("219", Taipei.StationName, Taichung.StationName, 2);
            Assert.AreEqual(2, tickets.Count());
            var t1 = tickets.First();
            var t2 = tickets.First(i => i != t1);
            Assert.IsTrue(IsNeighborSeat(t1, t2));
        }
        bool IsNeighborSeat(Ticket t1, Ticket t2)
        {
            bool sameCarbin = t1.Carbin == t2.Carbin;
            bool neighborseat = Seat.GetNeighbourSeatNo(t1.Seat) == t2.Seat;
            return sameCarbin && neighborseat;
        }
        [Test]
        public void BuyTicketTest_ThreeSeatThatTwoTogetherAddOne()
        {
            var tickets = ticketOperation.BuyTickets("219", Taipei.StationName, Taichung.StationName, 3);
            Assert.AreEqual(3, tickets.Count());
            //var t1 = tickets.First();
            //var t2 = tickets.First(i => i != t1);
            //Assert.IsTrue(IsNeighborSeat(t1, t2));
        }

        //[Test]
        //public void BuyTicketTest_WindowSide()
        //{
        //    Assert.Fail();
        //}
    }
}