using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Train;
using static Domain_Train.StationData;

namespace Domain_Train.dev
{
    public class FakeTrainDb : ITrainPersistant
    {
        public Dictionary<string, TrainData> dbContainer = new Dictionary<string, TrainData>();
        public void AddTrain(TrainData train)
        {
            dbContainer.Add(train.TrainID, train);
        }

        public void EditTrain(TrainData train)
        {
            dbContainer[train.TrainID] = train;
        }

        public TrainData GetTrain(string trainID)
        {
            var train = dbContainer[trainID];
            return new TrainData(train.TrainID, train.Type, null, train.Carbins, train.Stations);
        }

        public IEnumerable<TrainData> GetTrainsByID(string trainID, DateOnly date)
        {
            var r = new List<TrainData>();
            var train = dbContainer[trainID];
            if (!train.IsNoRunDay(date)) r.Add(new TrainData(train.TrainID, train.Type, train.NoRunDate, train.Carbins, train.Stations));

            return r;
        }

        public IEnumerable<TrainData> GetTrainsByStation(string stationName, DateOnly dateTime)
        {
            var r = dbContainer.Values.Where(
                i => i.IsNoRunDay(dateTime) == false &&
                i.Stations.Exists(s => s.StationName == stationName));
            return r;
        }

        public IEnumerable<TrainData> GetTrainsByTime(string startStation, string targetStation, DateOnly date, char scaleType, TimeOnly startTime, TimeOnly endTime)
        {
            var trains = dbContainer.Values.Where(i => i.IsNoRunDay(date) == false);
            trains = trains.Where(i =>
            i.Stations.Exists(s => s.StationName == startStation) &&
            i.Stations.Exists(s => s.StationName == targetStation)
            );
            trains = trains.Where(i => i.GetStationInfo(startStation).LeaveTime < i.GetStationInfo(targetStation).ArriveTime);
            if (scaleType == 'S')
            {
                trains = trains.Where(i => i.GetStationInfo(startStation).LeaveTime >= startTime)
                               .Where(i => i.GetStationInfo(startStation).LeaveTime <= endTime);
            }
            else if (scaleType == 'E')
            {
                trains = trains.Where(i => i.GetStationInfo(targetStation).ArriveTime >= startTime)
                               .Where(i => i.GetStationInfo(targetStation).ArriveTime <= endTime);
            }
            var r = trains.ToList();
            return r;
        }

        public void RemoveTrain(string trainID)
        {
            dbContainer.Remove(trainID);
        }

        public void InsertTestData()
        {
            //add train taruku402
            var taruku402_trainNo = "402";
            Carbin carbin1 = new Carbin(taruku402_trainNo, 16, 1);
            Carbin carbin2 = new Carbin(taruku402_trainNo, 16, 2);
            Carbin carbin3 = new Carbin(taruku402_trainNo, 16, 3);
            StationInfo taruku402_taipei = new StationInfo(Taipei, new TimeOnly(6, 11), new TimeOnly(6, 13));
            StationInfo taruku402_banqiao = new StationInfo(Banqiao, new TimeOnly(6, 1), new TimeOnly(6, 3));
            StationInfo taruku402_hualian = new StationInfo(Hualian, new TimeOnly(8, 19), new TimeOnly(8, 22));
            var taruku402 = new TrainData(taruku402_trainNo, TrainData.TrainType.自強, null, new Carbin[] { carbin1, carbin2, carbin3 }, new StationInfo[] { taruku402_banqiao, taruku402_taipei, taruku402_hualian });
            AddTrain(taruku402);
            //add train 272
            var zichiang272_trainNo = "272";
            Carbin zichiang272_carbin1 = new Carbin(zichiang272_trainNo, 16, 1);
            Carbin zichiang272_carbin2 = new Carbin(zichiang272_trainNo, 16, 2);
            Carbin zichiang272_carbin3 = new Carbin(zichiang272_trainNo, 16, 3);
            StationInfo zichiang272_taipei = new StationInfo(Taipei, new TimeOnly(7, 21), new TimeOnly(7, 24));
            StationInfo zichiang272_banqiao = new StationInfo(Banqiao, new TimeOnly(7, 08), new TimeOnly(7, 10));
            StationInfo zichiang272_Hualian = new StationInfo(Hualian, new TimeOnly(10, 42), new TimeOnly(10, 42));
            var zichiang272 = new TrainData(zichiang272_trainNo, TrainData.TrainType.自強, null, new Carbin[] { zichiang272_carbin1, zichiang272_carbin2, zichiang272_carbin3 }, new StationInfo[] { zichiang272_banqiao, zichiang272_taipei, zichiang272_Hualian });
            AddTrain(zichiang272);
            //add train 408
            var zichiang408_trainNo = "408";
            Carbin zichiang408_c1 = new Carbin(zichiang408_trainNo, 16, 1);
            Carbin zichiang408_c2 = new Carbin(zichiang408_trainNo, 16, 2);
            Carbin zichiang408_c3 = new Carbin(zichiang408_trainNo, 16, 3);
            StationInfo zichiang408_banqiao = new StationInfo(Banqiao, new TimeOnly(7, 13), new TimeOnly(7, 16));
            StationInfo zichiang408_taipei = new StationInfo(Taipei, new TimeOnly(7, 25), new TimeOnly(7, 30));
            StationInfo zichiang408_Hualian = new StationInfo(Hualian, new TimeOnly(9, 40), new TimeOnly(9, 43));
            var zichiang408 = new TrainData(zichiang408_trainNo, TrainData.TrainType.自強, null, new Carbin[] { zichiang408_c1, zichiang408_c2, zichiang408_c3 }, new StationInfo[] { zichiang408_taipei, zichiang408_banqiao, zichiang408_Hualian });
            AddTrain(zichiang408);
            //add train 511
            var 莒光511_trainNo = "511";
            Carbin 莒光511_c1 = new Carbin(莒光511_trainNo, 16, 1);
            Carbin 莒光511_c2 = new Carbin(莒光511_trainNo, 16, 2);
            Carbin 莒光511_c3 = new Carbin(莒光511_trainNo, 16, 3);
            StationInfo 莒光511_taipei = new StationInfo(Taipei, new TimeOnly(9, 13), new TimeOnly(9, 17));
            StationInfo 莒光511_banqiao = new StationInfo(Banqiao, new TimeOnly(9, 25), new TimeOnly(9, 27));
            StationInfo 莒光511_Taoyuan = new StationInfo(Taoyuan, new TimeOnly(9, 53), new TimeOnly(9, 54));
            var 莒光511 = new TrainData(莒光511_trainNo, TrainData.TrainType.莒光, null, new Carbin[] { 莒光511_c1, 莒光511_c2, 莒光511_c3 }, new StationInfo[] { 莒光511_taipei, 莒光511_banqiao, 莒光511_Taoyuan });
            AddTrain(莒光511);
            //Add Train 2193
            var 區間2193_trainNo = "2193";
            Carbin 區間2193_c1 = new Carbin(區間2193_trainNo, 16, 1);
            Carbin 區間2193_c2 = new Carbin(區間2193_trainNo, 16, 2);
            Carbin 區間2193_c3 = new Carbin(區間2193_trainNo, 16, 3);
            StationInfo 區間2193_taipei = new StationInfo(Taipei, new TimeOnly(12, 0), new TimeOnly(12, 02));
            StationInfo 區間2193_banqiao = new StationInfo(Banqiao, new TimeOnly(12, 11), new TimeOnly(12, 12));
            StationInfo 區間2193_Taoyuan = new StationInfo(Taoyuan, new TimeOnly(12, 39), new TimeOnly(12, 41));
            StationInfo 區間2193_Taichung = new StationInfo(Taichung, new TimeOnly(15, 35), new TimeOnly(15, 36));
            var 區間2193 = new TrainData(區間2193_trainNo, TrainData.TrainType.區間車, null, new Carbin[] { 區間2193_c1, 區間2193_c2, 區間2193_c3 }, new StationInfo[] { 區間2193_taipei, 區間2193_banqiao, 區間2193_Taoyuan, 區間2193_Taichung });
            AddTrain(區間2193);
            //add train 273
            var 普悠273_trainNo = "273";
            Carbin 普悠273_c1 = new Carbin(普悠273_trainNo, 16, 1);
            Carbin 普悠273_c2 = new Carbin(普悠273_trainNo, 16, 2);
            Carbin 普悠273_c3 = new Carbin(普悠273_trainNo, 16, 3);
            StationInfo 普悠273_hualian = new StationInfo(Hualian, new TimeOnly(13, 26), new TimeOnly(13, 26));
            StationInfo 普悠273_taipei = new StationInfo(Taipei, new TimeOnly(15, 41), new TimeOnly(15, 45));
            StationInfo 普悠273_banqiao = new StationInfo(Banqiao, new TimeOnly(15, 52), new TimeOnly(15, 53));
            StationInfo 普悠273_Taichung = new StationInfo(Taichung, new TimeOnly(17, 23), new TimeOnly(17, 25));
            var 普悠273 = new TrainData(普悠273_trainNo, TrainData.TrainType.區間車, null, new Carbin[] { 普悠273_c1, 普悠273_c2, 普悠273_c3 }, new StationInfo[] { 普悠273_hualian, 普悠273_taipei, 普悠273_banqiao, 普悠273_Taichung });
            AddTrain(普悠273);
        }
    }
}
