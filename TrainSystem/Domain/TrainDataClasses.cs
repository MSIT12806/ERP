using Microsoft.EntityFrameworkCore.Metadata;

namespace Train
{
    public abstract class TrainDataOperator
    {
        public abstract void AddTrainData();
        public abstract void EditTrainData();
        public abstract void RemoveTrainData();
    }

    public partial class TrainData
    {
        public string TrainID;
        public HashSet<DateOnly> NoRunDate = new HashSet<DateOnly>();
        public TrainType Type;
        public List<StationInfo> Stations;
        public List<Carbin> Carbins;
        public bool IsClockwise;
        #region Set train information
        public TrainData(string trainID, TrainType type, bool isClockwise, HashSet<DateOnly> noRunDayList, IEnumerable<Carbin> carbins, IEnumerable<StationInfo> stationList)
        {
            TrainID = trainID;
            Type = type;
            IsClockwise = isClockwise;
            NoRunDate = noRunDayList == null ? NoRunDate : new HashSet<DateOnly>(noRunDayList);
            Carbins = new List<Carbin>(carbins);
            Stations = new List<StationInfo>(stationList);
        }
        public void AddNoRunDate(DateOnly date)
        {
            NoRunDate.Add(date);
        }
        public void RemoveNoRunDate(DateOnly date)
        {
            NoRunDate.Remove(date);
        }
        #endregion

        #region Research / Get train information
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
        public bool IsNoRunDay(DateOnly date)
        {
            return NoRunDate.Contains(date);
        }

        public StationInfo GetStationInfo(string startStation)
        {
            return Stations.First(i => i.StationName == startStation);
        }
        #endregion
    }
    public partial class TrainData
    {
        public enum TrainType
        {
            自強,
            莒光,
            復興,
            太魯閣,
            普悠瑪,
            區間車,
            區間快車
        }
    }
    public class StationInfo
    {
        public string StationName;
        public TimeOnly ArriveTime;
        public TimeOnly LeaveTime;
        public StationInfo(string name, TimeOnly arrive, TimeOnly leave)
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
    public partial class Seat
    {
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
        public char State;
        public bool CanSell => State == Empty;
        public string SeatID => SeatNo.ToString();
        public string GetNeigborSeatID() { return ""; }
        public bool IsWindowSeat() { return false; }
    }
    public partial class Seat
    {
        public static char Empty => 'N';
        public static char Book => 'B';
        public static char Sold => 'Y';
    }
}