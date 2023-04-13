using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train;

namespace Domain_Train.dev
{
    public class Tools
    {
        public static Dictionary<DateOnly, List<TrainRunToStationInfo>> Get2023Datas(IEnumerable<TrainRunToStationInfo> trainRunToStationInfos)
        {
            Dictionary<DateOnly, List<TrainRunToStationInfo>> r = new Dictionary<DateOnly, List<TrainRunToStationInfo>>();
            for (DateTime i = new DateTime(2023, 1, 1); i < new DateTime(2024, 1, 1); i = i.AddDays(1))
            {
                if (i != new DateTime(2023, 10, 10))//測試資料：雙十節停駛
                    r.Add(DateOnly.FromDateTime(i), trainRunToStationInfos.ToList());
            }
            return r;
        }
    }
}
