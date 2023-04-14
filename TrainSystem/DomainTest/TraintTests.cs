using Domain_Train;
using Domain_Train.dev;
using Domain_Train.interfaces;
using NSubstitute;
using Train;
using static Domain_Train.StationDatas;
using static Train.TicketOperator;

namespace DomainTest
{
    public class SeatTests
    {
        ITrainPersistant TrainStore;
        TicketOperator ticketOperation;
        IStationPersistant StationStore;
        IDateTimeProvider dateTimeProvider;


        private TrainRunToStationInfo taipei;
        private TrainRunToStationInfo banqiao;
        private TrainRunToStationInfo taoyuan;
        private TrainRunToStationInfo taichung;
        private TrainRunToStationInfo kaohsiung;

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
            taipei = new TrainRunToStationInfo(Taipei.StationName, StationStore.GetStationNo(Taipei.StationName, k), new TimeOnly(6, 0), new TimeOnly(6, 0));
            banqiao = new TrainRunToStationInfo(Banqiao.StationName, StationStore.GetStationNo(Banqiao.StationName, k), new TimeOnly(6, 15), new TimeOnly(6, 17));
            taoyuan = new TrainRunToStationInfo(Taoyuan.StationName, StationStore.GetStationNo(Taoyuan.StationName, k), new TimeOnly(6, 55), new TimeOnly(6, 57));
            taichung = new TrainRunToStationInfo(Taichung.StationName, StationStore.GetStationNo(Taichung.StationName, k), new TimeOnly(7, 44), new TimeOnly(6, 46));
            kaohsiung = new TrainRunToStationInfo(Kaohsiung.StationName, StationStore.GetStationNo(Kaohsiung.StationName, k), new TimeOnly(10, 13), new TimeOnly(10, 15));
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
        //[Test] public void EmptySeatInitializeTest() { }
        [Test]
        public void CanSellTest()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            //act
            //assert
            Assert.IsTrue(seat.CanSell(taipei.StationNo, taoyuan.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now())));
        }
        [Test]
        public void CanSellTest_SellOutAPartNotInRange_ReturnTrue()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            //act
            seat.BookThisSeat(banqiao.StationNo, taoyuan.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()));
            //assert
            Assert.IsTrue(seat.CanSell(taipei.StationNo, banqiao.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now())));
        }
        [Test]
        public void CanSellTest_SellOutAPart_ReturnFalse()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            //act
            seat.BookThisSeat(banqiao.StationNo, taoyuan.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()));
            //assert
            Assert.IsFalse(seat.CanSell(taipei.StationNo, taoyuan.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now())));
        }
        [Test]
        public void IsWindowSideTest()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            //act
            //assert
            Assert.IsTrue(seat.IsWindowSide());
        }
        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(4, 2)]
        [TestCase(9, 11)]
        [TestCase(12, 10)]
        public void GetNeighborSeaNoTest(int seatNo, int neighbourNo)
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(seatNo);
            //act
            var nSeatNo = seat.GetNeighbourSeatNo();
            //assert
            Assert.AreEqual(neighbourNo, nSeatNo);
        }
        [Test]
        public void IsNeighborSeatFreeTest()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            //act
            var unsoldSeats = train.GetUnsoldSeatsDescendingOrderByCarbinEmptySeatCount(taipei.StationNo, taoyuan.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()));
            //assert
            Assert.IsTrue(seat.IsNeighbourSeatFree(unsoldSeats));
        }
        [Test]
        public void IsNeighborSeatFreeTest_BookNeighborSeat_ReturnFalse()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            var nSeat = train.GetCarbin(1).GetSeat(3);
            //act
            nSeat.BookThisSeat(taipei.StationNo, banqiao.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()));
            var unsoldSeats = train.GetUnsoldSeatsDescendingOrderByCarbinEmptySeatCount(taipei.StationNo, taoyuan.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()));
            //assert
            Assert.IsFalse(seat.IsNeighbourSeatFree(unsoldSeats));
        }
        [Test]
        public void IsNeighborSeatFreeTest_BookNeighborSeatNotInRange_ReturnTrue()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            var nSeat = train.GetCarbin(1).GetSeat(3);
            //act
            nSeat.BookThisSeat(taoyuan.StationNo, taichung.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()));
            var unsoldSeats = train.GetUnsoldSeatsDescendingOrderByCarbinEmptySeatCount(taipei.StationNo, taoyuan.StationNo, DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()));
            //assert
            Assert.IsTrue(seat.IsNeighbourSeatFree(unsoldSeats));
        }
        //[Test] public void BookThisSeatTest() { }
    }
    public class CarbinTests
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
        public void GetNeighborSeat()
        {
            //arrange
            var train = TrainStore.GetTrain("219");
            var seat = train.GetCarbin(1).GetSeat(1);
            //act
            var nSeat = train.GetCarbin(1).GetNeighbourSeat(1);
            //assert
            Assert.AreEqual(nSeat.SeatNo, 3);
        }
    }
    public class TrainTests
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

        //[Test]
        //public void PreTest()
        //{
        //}

        //[Test] public void ArriveTimeTest() { }
        //[Test] public void LeaveTimeTest() { }
        //[Test] public void IsNoRunDayTest() { }
        //[Test] public void GetStationInfoTest() { }
        //[Test] public void GetUnsoldSeatsTest() { }
        //[Test] public void GetUnsoldSeatTest() { }

    }
}