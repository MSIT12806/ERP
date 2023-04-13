using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Domain_Train.Station;

namespace Domain_Train
{
    public interface IStationPersistant
    {
        Station GetStation(string name);
        int GetStationNo(string name, TrunkLine trunkLine);
        IEnumerable<int> GetStationNoList(string start, string end, TrunkLine trunkLine);
    }
    public class FakeStationDB : IStationPersistant
    {
        public static List<Station> AllStations = new List<Station>();
        static bool _initialized = false;
        public FakeStationDB()
        {
            Initialize();
        }
        void Initialize()
        {
            if (_initialized) return;

            // 取得 StationDatas.Station 型別的所有欄位
            var fields = typeof(StationDatas).GetFields(BindingFlags.Static | BindingFlags.Public);

            // 將所有欄位的值加入 AllStations 列表當中
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Station))
                {
                    AllStations.Add((Station)field.GetValue(null));
                }
            }
            _initialized = true;
        }
        public Station GetStation(string stationName)
        {
            return AllStations.First(i => i.StationName == stationName);
        }
        public int GetStationNo(string stationName, TrunkLine trunkLine)
        {
            var station = AllStations.First(i => i.StationName == stationName);
            var stationNo = station.StationNo[trunkLine];
            return stationNo;
        }
        public IEnumerable<int> GetStationNoList(string start, string end, TrunkLine trunkLine)
        {
            var sStation = GetStation(start);
            var eStation = GetStation(end);
            IEnumerable<int> list = null;
            if (eStation.StationNo[trunkLine] > sStation.StationNo[trunkLine])
            {
                list = AllStations.Where(s => s.StationNo[trunkLine] >= sStation.StationNo[trunkLine] && s.StationNo[trunkLine] <= eStation.StationNo[trunkLine]).Select(i => i.StationNo[trunkLine]);
            }
            else
            {
                list = AllStations.Where(s => s.StationNo[trunkLine] <= sStation.StationNo[trunkLine] && s.StationNo[trunkLine] >= eStation.StationNo[trunkLine]).Select(i => i.StationNo[trunkLine]);
            }

            return list;
        }
    }
}
