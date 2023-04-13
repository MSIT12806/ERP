using Domain_Train;
using System.Collections.Generic;
using static Domain_Train.Station;

namespace Train
{
    public abstract class TrainDataOperator
    {
        public abstract void AddTrainData();
        public abstract void EditTrainData();
        public abstract void RemoveTrainData();
    }
    /// <summary>
    /// 班次資料
    /// </summary>
    public partial class TrainData
    {
        #region Values
        public string TrainID;
        public Dictionary<DateOnly, List<TrainRunToStationInfo>> RunInfos = new Dictionary<DateOnly, List<TrainRunToStationInfo>>();
        public TrunkLine TrunkLine;
        public bool IsForward;//forward or back
        public TrainType Type;
        public List<Carbin> Carbins;
        #endregion
        #region Set train information
        public TrainData(string trainID, Station.TrunkLine trunkLine, TrainType type, bool isForward, Dictionary<DateOnly, List<TrainRunToStationInfo>> runInfos, IEnumerable<Carbin> carbins)
        {
            TrainID = trainID;
            TrunkLine = trunkLine;
            Type = type;
            IsForward = isForward;
            this.RunInfos = runInfos == null ? this.RunInfos : new Dictionary<DateOnly, List<TrainRunToStationInfo>>(runInfos);
            Carbins = new List<Carbin>(carbins.Select(c => c.GetNewOne()));

            foreach (var c in Carbins)
            {
                foreach (var seat in c.Seats)
                {
                    foreach (var date in runInfos.Keys)
                    {
                        seat.EmptySeatInitialize(date,
                    runInfos[date].Select(i => i.StationNo).ToArray()
                    );
                    }

                }
            }
        }
        #endregion

        #region Research / Get train information
        public Carbin GetCarbin(int carbinNo)
        {
            return Carbins[carbinNo - 1];
        }
        public TimeOnly ArriveTime(string station, DateOnly date)
        {
            var stationData = RunInfos[date].First(i => i.StationName == station);
            return stationData.ArriveTime;
        }
        public TimeOnly LeaveTime(string station, DateOnly date)
        {
            var stationData = RunInfos[date].First(i => i.StationName == station);
            return stationData.LeaveTime;
        }
        public bool IsNoRunDay(DateOnly date)
        {
            return RunInfos.ContainsKey(date);
        }

        public TrainRunToStationInfo GetStationInfo(string startStation, DateOnly date)
        {
            return RunInfos[date].First(i => i.StationName == startStation);
        }

        public IEnumerable<Seat> GetUnsoldSeats(string startStation, string targetStation, TrainType type, DateOnly date)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Seat> GetUnsoldSeats(int startStation, int targetStation, DateOnly date)
        {
            var seats = Carbins.SelectMany(i => i.Seats.Where(s => s.CanSell(startStation, targetStation, date)));//把linq結構拆出來寫
            return seats;
        }

        public Seat GetUnsoldSeat(int startStation, int targetStation, DateOnly date)
        {
            var seat = Carbins.SelectMany(i => i.Seats.Where(s => s.CanSell(startStation, targetStation, date))).FirstOrDefault(); //把linq結構拆出來寫
            return seat;
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
    public class TrainRunToStationInfo
    {
        public string StationName;
        public int StationNo;
        public TimeOnly ArriveTime;
        public TimeOnly LeaveTime;
        public TrainRunToStationInfo(string name, int no, TimeOnly arrive, TimeOnly leave)
        {
            StationName = (name);
            StationNo = no;
            ArriveTime = arrive;
            LeaveTime = leave;
        }
    }

    public class Carbin
    {
        public List<Seat> Seats;
        private int _seatCount;
        private string _trainNo;
        public Carbin GetNewOne()
        {
            return new Carbin(_trainNo, _seatCount, Seats.First().Carbin);
        }
        public Carbin(string trainID, int seatCount, int carbinNo)
        {
            this._trainNo = trainID;
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
            SeatNo = seatNo;
            Carbin = carbin;
        }

        public void EmptySeatInitialize(DateOnly date, IEnumerable<int> stationNos)
        {
            if (States.ContainsKey(date)) throw new InvalidOperationException("這天的 Seat 已經初始化了");

            Dictionary<int, char> keyStatePairs = new Dictionary<int, char>();
            foreach (var no in stationNos)
            {
                keyStatePairs.Add(no, Seat.Empty);
            }
        }

        public int SeatNo { get; private set; }
        public int Carbin { get; private set; }
        public Dictionary<DateOnly, Dictionary<int, char>> States = new Dictionary<DateOnly, Dictionary<int, char>>();
        public bool CanSell(int startStation, int targetStation, DateOnly date)
        {
            int max = Math.Max(startStation, targetStation);
            int min = Math.Min(startStation, targetStation);
            for (int i = min; i <= max; i++)
            {
                if (States.ContainsKey(date) && States[date].ContainsKey(i))
                    if (States[date][i] != Empty) return false;
            }
            return true;
        }
        public string SeatID => SeatNo.ToString();
        public Seat GetNeighbourSeat() { return null; }
        public bool IsWindowSeat() { return false; }

        public bool IsNeighbourSeatFree()
        {
            throw new NotImplementedException();
        }

        public decimal BookThisSeat(int startStation, int targetStation, DateOnly date)
        {
            /*
票價計算原則
本局各級列車票價費率：(各車種現行起碼里程為10公里計價，不滿10公里，以10公里計價。)
普通（快）車：每人每公里1.06元。
復興號/區間車：每人每公里1.46元。
莒光號：每人每公里1.75元。
自強號：每人每公里2.27元。
山海線計價方式：
由竹南以北各站至彰化以南各站或由彰化以南各站至竹南以北各站之票價，一律按經由山線之里程計算。
由竹南以北或彰化以南各站至山、海線各站或由山、海線各站至竹南以北或彰化以南各站之票價。一律 按最短里程計算。
由山線各站至海線各站，或由海線各站至山線各站之票價，一律按列車行駛方向實際里程計算。
自強3000騰雲座艙票價計費方式敬請參考票價試算。(起碼里程為50公里計價，售票系統不予發售未達50公里之騰雲座艙車票。)

成人票價按乘車區間營業里程乘票價率計算之。
孩童指未滿12歲之人。孩童身高滿150公分者，應購成人票，未滿115公分者免費，滿115公分未滿150公分者，應購孩童票，票價按成人票價半數，尾數四捨五入後計收。前項孩童滿115 公分而未滿 6歲者，經出示身分證件，得免費；滿150公分而未滿12歲者，經出示身分證件，得購買孩童票。免費孩童但佔座位者，及由持有乘車票旅客隨帶孩童人數超過2人者，其超過之孩童，仍應購孩童票。
敬老票價(年滿65歲之本國國民或於永久居留證並註記搭乘國內大眾交通工具優待之永久居留權人士)及身心障礙優待票價(持有主管機關核發之身心障礙證明（手冊）者)按成人票價半數尾數四捨五入後計收。
八十一公里(含)以上莒光號、復興號對號列車無座票八折計價優待。
減價優待票以一項優待為限，不得重複優待。
各種減價優待票價不足起碼票價時，按起碼票價計收。(去回票起迄站不滿10公里者，仍以原單程票價加倍後計收。)
身心障礙者乘車，須於身心障礙證明『必要陪伴者優惠措施』欄位中註記有『國內大眾運輸工具』字樣者，其必要之陪伴者一人得享有愛心票優惠。

電子票價計算原則
電子票證票價計算方式與本局現行乘車票計算方式相同，依搭乘車種對應之票價計算方式計價，成人(全票)按下列票價全額計收；兒童、敬老及愛心票按下列票價半價計收：
搭乘莒光號、復興號及區間車旅客：按起、迄站區間車票價9折計價。
搭乘自強號旅客：
乘車於70公里(含)內，按起、迄站區間車票價9折計算。
乘車超過70公里，票價分為以下2段計算後加總計價：
第1段：前70公里以區間車票價9折計算。
第2段：超出70公里部分，超出里程以自強號費率(每公里2.27元)計算。
自強號乘車70公里(含)內之認定：
非指自搭乘自強號起算70公里，而是總合各級列車之搭乘及轉乘在70公里以內，均收取區間車票價並享有電子票證優惠。搭乘自強號於迄站時總里程超過70公里以上(包含所有轉乘)，即就超過里程加計自強號與區間車差額，莒光號(含)以下列車不額外補收。
             */


            //if (!CanSell(startStation, targetStation)) throw new Notify("此座位不能販售"); 放給外面檢查
            int max = Math.Max(startStation, targetStation);
            int min = Math.Min(startStation, targetStation);
            for (int i = min; i <= max; i++)
            {
                if (States.ContainsKey(date) && States[date].ContainsKey(i))
                    if (States[date][i] != Empty) return 0;
            }
            return 0;
        }
    }
    public partial class Seat
    {
        public static char Empty => 'N';
        public static char Book => 'B';
        public static char Sold => 'Y';
    }
}