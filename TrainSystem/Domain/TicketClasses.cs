using System;

namespace Train
{
    public class TicketOperator
    {
        ITrainPersistant Repository;
        public TicketOperator(ITrainPersistant repository)
        {
            this.Repository = repository;
        }
        public Ticket BuyTicket(string trainID, string startStation, string targetStation, int carbin, int seat, DateTime dateTime = default(DateTime))
        {
            GetNowWhenDateTimeIsDefault(ref dateTime);
            TrainData train = Repository.GetTrain(trainID);
            //Ticket result = new Ticket();
            throw new NotImplementedException();
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
            public TimeOnly StartTime;
            public string TargetStation;
            public TimeOnly ArriveTime;
            public string TrainID;
            public string Carbin;
            public string Seat;
            public DateTime ExpirationDate;
            public decimal Price;
            public Ticket(string startStation, TimeOnly startTime, string targetStation, TimeOnly arriveTime, string trainID, string carbin, string seat, DateTime expirationDate, decimal price)
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