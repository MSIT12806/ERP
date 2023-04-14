using Domain_Train;
using NSubstitute;
using Train;

namespace DomainTest
{
    public class UseCase_StationResearchTests
    {
        IStationPersistant StationStore;
        [SetUp]
        public void Setup()
        {
            StationStore = new FakeStationDB();
        }

        [Test]
        public void GetStationNoTest()
        {
            int BanqiaoClockwiseNo = 1141;

            Assert.AreEqual(BanqiaoClockwiseNo, StationStore.GetStationNo(StationDatas.Banqiao.StationName, Station.TrunkLine.環島線順));
        }

    }
}