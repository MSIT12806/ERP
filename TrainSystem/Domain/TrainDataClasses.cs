namespace Train
{
    public abstract class TrainDataOperator
    {
        public abstract void AddTrainData();
        public abstract void EditTrainData();
        public abstract void RemoveTrainData();
    }

    public class TrainData
    {
        public string TrainID;
        public List<TrainTime> Stations;
        public List<Carbin> Carbins;

        public TrainData(string trainID, IEnumerable<Carbin> carbins, IEnumerable<TrainTime> stationList)
        {
            TrainID = trainID;
            Carbins = carbins.ToList();
            Stations = stationList.ToList();
        }
        public Carbin GetCarbin(int carbinNo)
        {
            return Carbins[carbinNo - 1];
        }
        public Seat SellFreeSeat(int carbin, int seat)
        {
            var carb = GetCarbin(carbin);
            var r = carb.GetSeat(seat);
            if (r.CanSell)
                return r;

            throw new InvalidOperationException("this seat can not be sold");
        }
        public TimeOnly ArriveTime(string station)
        {
            var stationData = Stations.First(i => i.StationName == station);
            return stationData.ArriveTime;
        }
        public TimeOnly LeaveTime(string station)
        {
            var stationData = Stations.First(i => i.StationName == station);
            return stationData.LeaveTime;
        }
    }

    public class TrainTime
    {
        public string StationName;
        public TimeOnly ArriveTime;
        public TimeOnly LeaveTime;
        public TrainTime(string name, TimeOnly arrive, TimeOnly leave)
        {
            StationName = name;
            ArriveTime = arrive;
            LeaveTime = leave;
        }
    }

    public class Carbin
    {
        public List<Seat> Seats;
        private int _seatCount;
        private string _trainNo;

        public Carbin(string trainNo, int seatCount, int carbinNo)
        {
            this._trainNo = trainNo;
            this._seatCount = seatCount;
            Seats = new List<Seat>();
            for (int i = 1; i <= seatCount; i++)
            {
                Seats.Add(new Seat(i, carbinNo));
            }
        }

        public Seat GetSeat(int seat)
        {
            return Seats[seat - 1];//OutOfRangeException
        }
    }
    public class Seat
    {
        public static readonly char Empty = 'N';
        public static readonly char Book = 'B';
        public static readonly char Sold = 'Y';
        public Seat(int seatNo, int carbin)
        {
            EmptySeatInitialize(seatNo, carbin);
        }

        private void EmptySeatInitialize(int seatNo, int carbin)
        {
            SeatNo = seatNo;
            Carbin = carbin;
            State = Empty;
        }

        public int SeatNo { get; private set; }
        public int Carbin { get; private set; }
        public char State;// N / Y / B
        public bool CanSell => Sold == Empty;
        public string SeatID => SeatNo.ToString();
        public string GetNeigborSeatID() { return ""; }
        public bool IsWindowSeat() { return false; }
    }
}