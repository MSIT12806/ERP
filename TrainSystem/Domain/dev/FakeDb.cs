using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train;

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
            return new TrainData(train.TrainID, train.Carbins, train.Stations);
        }

        public IEnumerable<TrainData> GetTrains(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainData> GetTrainsByID(string trainID, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainData> GetTrainsByStation(string stationName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void RemoveTrain(string trainID)
        {
            dbContainer.Remove(trainID);
        }
    }
}
