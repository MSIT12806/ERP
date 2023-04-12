using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Domain_Train.Station;

namespace Domain_Train
{
    public class Station
    {
        public enum TrunkLine
        {
            環島線順 = 1,
            環島線逆,
            集集線順,//二水->源泉->濁水->龍泉->集集->水里->車埕
            集集線逆,
            //平溪線順, 
            //平溪線逆, 
            //深澳線順, 
            //深澳線逆, 
            //內灣線順, 
            //內灣線逆, 
            //六家線順, 
            //六家線逆, 
            //沙崙線順, 
            //沙崙線逆, 
            //舊山線順,
            //舊山線逆
        }
        public Station(string name, params (TrunkLine lineType, int no)[] noPairs)
        {
            StationName = name;
            StationNo = new Dictionary<TrunkLine, int>();
            foreach (var (t, n) in noPairs)
            {
                StationNo.Add(t, n);
            }
        }
        public Dictionary<TrunkLine, int> StationNo { get; }
        public string StationName { get; }
    }
    public class StationDatas
    {
        public static readonly Station Taipei = new Station("taipei", (TrunkLine.環島線逆, 1001), (TrunkLine.環島線順, 1001));
        public static readonly Station Banqiao = new Station("banqiao", (TrunkLine.環島線逆, 1011), (TrunkLine.環島線順, 1141));
        public static readonly Station Taoyuan = new Station("taoyuan", (TrunkLine.環島線逆, 1011), (TrunkLine.環島線順, 1125));
        public static readonly Station Taichung = new Station("taichung", (TrunkLine.環島線逆, 1011), (TrunkLine.環島線順, 1111));
        public static readonly Station Kaohsiung = new Station("kaohsiung", (TrunkLine.環島線逆, 1011), (TrunkLine.環島線順, 1081));
        public static readonly Station Hualian = new Station("hualian", (TrunkLine.環島線逆, 1011), (TrunkLine.環島線順, 1041));
        public static List<Station> AllStations = new List<Station>();
        static bool _initialized = false;
        public static void Initialize()
        {
            if (_initialized) return;

            // 取得 StationDatas.Station 型別的所有欄位
            var fields = typeof(StationDatas).GetFields(BindingFlags.Static | BindingFlags.Public);

            // 將所有欄位的值加入 AllStations 列表當中
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Station))
                {
                    StationDatas.AllStations.Add((Station)field.GetValue(null));
                }
            }
            _initialized = true;
        }
        public static int GetStationNo(string stationName, TrunkLine trunkLine)
        {
            var station = AllStations.First(i => i.StationName == stationName);
            var stationNo = station.StationNo[trunkLine];
            return stationNo;
        }
    }
}
