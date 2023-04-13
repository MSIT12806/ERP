using Domain_Train;
using Domain_Train.dev;
using Domain_Train.interfaces;
using NSubstitute;
using Train;
using static Domain_Train.StationDatas;
using static Train.TicketOperator;

namespace DomainTest
{
    public class DateTimeProviderTests
    {
        IDateTimeProvider dateTimeProvider;
        [SetUp]
        public void Setup()
        {
            dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.Now().Returns(new DateTime(2023, 4, 12, 3, 4, 5));
            MyDateTimeProvider.Ins = dateTimeProvider;
        }

        [Test]
        public void PreTest()
        {
            Assert.AreEqual(new DateTime(2023, 4, 12, 3, 4, 5), MyDateTimeProvider.Ins.Now());
        }

    }
}