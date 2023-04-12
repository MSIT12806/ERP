using Domain_Train;
using NSubstitute;
using Train;

namespace DomainTest
{
    public class StationResearchTests
    {
        [SetUp]
        public void Setup()
        {
            StationDatas.Initialize();
        }

        [Test]
        public void GetStationNoTest()
        {
            int BanqiaoClockwiseNo = 1141;

            Assert.AreEqual(BanqiaoClockwiseNo, StationDatas.GetStationNo(StationDatas.Banqiao.StationName, Station.TrunkLine.環島線順));
        }

    }
}