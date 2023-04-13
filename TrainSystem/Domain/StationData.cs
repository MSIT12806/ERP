using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Domain_Train.Station;
using static Train.TrainData;

namespace Domain_Train
{
    public class Station
    {
        public enum LevelType
        {
            特等,
            一等,
            二等,
            三等,
            簡易,
            招呼,
            號誌
        }
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
        public Station(string name, LevelType level, params (TrunkLine lineType, int no)[] noPairs)
        {
            StationName = name;
            Level = level;
            StationNo = new Dictionary<TrunkLine, int>();
            foreach (var (t, n) in noPairs)
            {
                StationNo.Add(t, n);
            }
        }
        public LevelType Level;
        public Dictionary<TrunkLine, int> StationNo { get; }
        public string StationName { get; }
    }
    public class StationDatas
    {
        public static readonly Station Taipei = new Station("taipei",LevelType.特等, (TrunkLine.環島線逆, 1001), (TrunkLine.環島線順, 1001));
        public static readonly Station Banqiao = new Station("banqiao",LevelType.一等, (TrunkLine.環島線逆, 1011), (TrunkLine.環島線順, 1141));
        public static readonly Station Taoyuan = new Station("taoyuan",LevelType.一等, (TrunkLine.環島線逆, 1025), (TrunkLine.環島線順, 1125));
        public static readonly Station Taichung = new Station("taichung",LevelType.一等, (TrunkLine.環島線逆, 1040), (TrunkLine.環島線順, 1111));
        public static readonly Station Kaohsiung = new Station("kaohsiung", LevelType.特等, (TrunkLine.環島線逆, 1080), (TrunkLine.環島線順, 1081));
        public static readonly Station Hualian = new Station("hualian", LevelType.特等, (TrunkLine.環島線逆, 1111), (TrunkLine.環島線順, 1041));
       
     
    }
}
