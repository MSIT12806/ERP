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
        public IEnumerable<Ticket> BuyTickets(string trainID, string startStation, string targetStation, int ticketCount, DateTime dateTime = default(DateTime))
        {
            //兩兩售票
            //如果剩下單張，就盡量以鄰座已售出的賣
            List<Ticket> result = new List<Ticket>();
            TrainData train = TrainFinder.GetTrainsByID(trainID, DateOnly.FromDateTime(dateTime)).FirstOrDefault();
            if (train == null) return result;

            TrainRunToStationInfo startStationInfo = train.GetStationInfo(startStation, DateOnly.FromDateTime(dateTime));
            TrainRunToStationInfo targetStationInfo = train.GetStationInfo(targetStation, DateOnly.FromDateTime(dateTime));
            IEnumerable<Seat> freeSeats = train.GetUnsoldSeats(startStationInfo.StationNo, targetStationInfo.StationNo, train.Type, dateTime);
            var freeSeatCount = freeSeats.Count();//如果要這樣搞，是不是就得lock住?
            if (freeSeatCount < ticketCount) throw new Notify("沒有足夠的座位");

            while (ticketCount > 0)
            {
                if (ticketCount > 1)
                {
                    Seat seat1 = freeSeats.First(i => i.IsNeighbourSeatFree());
                    Seat seat2 = seat1.GetNeighbourSeat();
                    result.Add(new Ticket(startStation, startStationInfo.ArriveTime, targetStation, targetStationInfo.ArriveTime, trainID, seat1.Carbin, seat1.SeatNo, dateTime, seat1.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo)));
                    result.Add(new Ticket(startStation, startStationInfo.ArriveTime, targetStation, targetStationInfo.ArriveTime, trainID, seat2.Carbin, seat2.SeatNo, dateTime, seat1.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo)));
                    ticketCount -= 2;

                }
                else
                {

                    var seat = freeSeats.FirstOrDefault(i => !i.IsNeighbourSeatFree());
                    seat = seat == null ? freeSeats.FirstOrDefault(i => i.IsNeighbourSeatFree()) : seat;
                    result.Add(new Ticket(startStation, startStationInfo.ArriveTime, targetStation, targetStationInfo.ArriveTime, trainID, seat.Carbin, seat.SeatNo, dateTime, seat.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo)));
                    ticketCount -= 1;
                }
            }
            return result;
        }
        public Ticket BuyTicket(string trainID, string startStation, string targetStation, DateTime dateTime = default(DateTime))
        {
            GetNowWhenDateTimeIsDefault(ref dateTime);
            TrainData train = TrainFinder.GetTrain(trainID);
            TrainRunToStationInfo startStationInfo = train.GetStationInfo(startStation, DateOnly.FromDateTime(dateTime));
            TrainRunToStationInfo targetStationInfo = train.GetStationInfo(targetStation, DateOnly.FromDateTime(dateTime));
            //todo: 如果是過去日期或是今天已經過站了，就不准賣。
            Seat unsoldSeat = train.GetUnsoldSeat(startStationInfo.StationNo, targetStationInfo.StationNo, train.Type, dateTime);
            Ticket result = new Ticket(startStation, train.LeaveTime(startStation, DateOnly.FromDateTime(dateTime)), targetStation, train.ArriveTime(targetStation, DateOnly.FromDateTime(dateTime)), train.TrainID, unsoldSeat.Carbin, unsoldSeat.SeatNo, dateTime, 100);
            unsoldSeat.BookThisSeat(startStationInfo.StationNo, targetStationInfo.StationNo);
            return result;
        }

        private TrainData GetTrain(string trainID)
        {

            throw new NotImplementedException();
        }

        private void GetNowWhenDateTimeIsDefault(ref DateTime dateTime)
        {
            dateTime = dateTime == default(DateTime) ? DateTime.Now : dateTime;
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
            public DateTime ExpirationDate;//訂票到期日 / 乘車到期日
            public decimal Price;
            public Ticket(string startStation, TimeOnly startTime, string targetStation, TimeOnly arriveTime, string trainID, int carbin, int seat, DateTime expirationDate, decimal price)
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