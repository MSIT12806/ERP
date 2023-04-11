using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Train;
using static Domain_Train.StationData;

namespace Domain_Train.dev
{
    public class EF : DbContext, ITrainPersistant
    {
        public DbSet<TrainData> TrainDatas { get; set; }

        public void AddTrain(TrainData train)
        {
            TrainDatas.Add(train);
            this.SaveChanges();
        }

        public void EditTrain(TrainData train)
        {
            TrainDatas.Update(train);
            this.SaveChanges();
        }

        public TrainData GetTrain(string trainID)
        {
            return TrainDatas.Find(trainID);
        }

        public IEnumerable<TrainData> GetTrainsByID(string trainID, DateOnly date)
        {
            var r = new List<TrainData>();
            var train = GetTrain(trainID);
            if (!train.IsNoRunDay(date)) r.Add(new TrainData(train.TrainID, train.Type, false, train.NoRunDate, train.Carbins, train.Stations));

            return r;
        }

        public IEnumerable<TrainData> GetTrainsByStation(string stationName, DateOnly dateTime)
        {
            var r = TrainDatas.Where(
                        i => i.IsNoRunDay(dateTime) == false &&
                        i.Stations.Exists(s => s.StationName == stationName));
            return r;
        }

        public IEnumerable<TrainData> GetTrainsByTime(string startStation, string targetStation, DateOnly date, char scaleType, TimeOnly startTime, TimeOnly endTime)
        {
            var trains = TrainDatas.Where(i => i.IsNoRunDay(date) == false);
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
            TrainDatas.Remove(GetTrain(trainID));
            this.SaveChanges();
        }
    }
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
            return new TrainData(train.TrainID, train.Type, false, null, train.Carbins, train.Stations);
        }

        public IEnumerable<TrainData> GetTrainsByID(string trainID, DateOnly date)
        {
            var r = new List<TrainData>();
            var train = dbContainer[trainID];
            if (!train.IsNoRunDay(date)) r.Add(new TrainData(train.TrainID, train.Type, false, train.NoRunDate, train.Carbins, train.Stations));

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


    }
}
