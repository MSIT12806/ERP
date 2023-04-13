using Domain_Train;
using System;

namespace Train
{
    public class TicketOperator
    {
        TrainFinder TrainFinder;
        public TicketOperator(TrainFinder finder)
        {
            this.TrainFinder = finder;
        }
        /// <summary>
        /// 錯誤：回傳通知
        /// </summary>
        /// <exception cref="Notify">回傳通知</exception>
        public IEnumerable<Ticket> BuyTickets(string trainID, string startStation, string targetStation, int ticketCount, DateOnly date = default(DateOnly))
        {
            //兩兩售票
            //如果剩下單張，就盡量以鄰座已售出的賣
            List<Ticket> result = new List<Ticket>();
            TrainData train = TrainFinder.GetTrainByID(trainID, date);
            if (train == null) return result;

            TrainRunToStationInfo startStationInfo = train.GetStationInfo(startStation, date);
            TrainRunToStationInfo targetStationInfo = train.GetStationInfo(targetStation, date);
            IEnumerable<Seat> freeSeats = train.GetUnsoldSeats(startStationInfo.StationNo, targetStationInfo.StationNo, date);
            var freeSeatCount = freeSeats.Count();//如果要這樣搞，是不是就得lock住?
            if (freeSeatCount < ticketCount) throw new Notify("沒有足夠的座位");

            while (ticketCount > 0)
            {
                if (ticketCount > 1)
                {
                    Seat seat1 = freeSeats.First(i => i.IsNeighbourSeatFree());
                    Seat seat2 = seat1.GetNeighbourSeat();
                    result.Add(new Ticket(startStation, startStationInfo.ArriveTime, targetStation, targetStationInfo.ArriveTime, trainID, seat1.Carbin, seat1.SeatNo, date, seat1.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo, date)));
                    result.Add(new Ticket(startStation, startStationInfo.ArriveTime, targetStation, targetStationInfo.ArriveTime, trainID, seat2.Carbin, seat2.SeatNo, date, seat1.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo, date)));
                    ticketCount -= 2;

                }
                else
                {

                    var seat = freeSeats.FirstOrDefault(i => !i.IsNeighbourSeatFree());
                    seat = seat == null ? freeSeats.FirstOrDefault(i => i.IsNeighbourSeatFree()) : seat;
                    result.Add(new Ticket(startStation, startStationInfo.ArriveTime, targetStation, targetStationInfo.ArriveTime, trainID, seat.Carbin, seat.SeatNo, date, seat.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo, date)));
                    ticketCount -= 1;
                }
            }
            return result;
        }
        public Ticket BuyTicket(string trainID, string startStation, string targetStation, DateOnly date = default(DateOnly))
        {
            GetNowWhenDateTimeIsDefault(ref date);
            TrainData train = TrainFinder.GetTrainByID(trainID, date);
            TrainRunToStationInfo startStationInfo = train.GetStationInfo(startStation, date);
            TrainRunToStationInfo targetStationInfo = train.GetStationInfo(targetStation, date);
            if (date < DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now())) throw new Notify("不可販售過期的票");
            Seat unsoldSeat = train.GetUnsoldSeat(startStationInfo.StationNo, targetStationInfo.StationNo, date);
            Ticket result = new Ticket(startStation, train.LeaveTime(startStation, date), targetStation, train.ArriveTime(targetStation, date), train.TrainID, unsoldSeat.Carbin, unsoldSeat.SeatNo, date, 100);
            unsoldSeat.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo, date);
            return result;
        }

        private TrainData GetTrain(string trainID)
        {

            throw new NotImplementedException();
        }

        private void GetNowWhenDateTimeIsDefault(ref DateOnly date)
        {

            date = date == default(DateOnly) ? DateOnly.FromDateTime(MyDateTimeProvider.Ins.Now()) : date;
        }

        public class Ticket
        {
            public string StartStation;
            public TimeOnly StartTime;//給乘客看的
            public string TargetStation;
            public TimeOnly ArriveTime;//給乘客看的
            public string TrainID;
            public int Carbin;
            public int Seat;
            public char State;//購票狀態
            public DateOnly ExpirationDate;//訂票到期日 / 乘車到期日
            public decimal Price;
            public Ticket(string startStation, TimeOnly startTime, string targetStation, TimeOnly arriveTime, string trainID, int carbin, int seat, DateOnly expirationDate, decimal price)
            {
                this.StartStation = startStation;
                this.StartTime = startTime;
                this.TargetStation = targetStation;
                this.TrainID = trainID;
                this.Carbin = carbin;
                this.Seat = seat;
                this.ExpirationDate = expirationDate;
                this.Price = price;
            }
        }
    }
}