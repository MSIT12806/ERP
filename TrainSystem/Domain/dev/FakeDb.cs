using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Train;
using static Domain_Train.StationDatas;

namespace Domain_Train.dev
{
    public class TrainEF : DbContext, ITrainPersistant
    {
        public TrainEF(DbContextOptions<TrainEF> options) : base(options) { }
        public DbSet<TrainData> TrainDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }
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

        public void RemoveTrain(string trainID)
        {
            TrainDatas.Remove(GetTrain(trainID));
            this.SaveChanges();
        }

        public IEnumerable<TrainData> GetTrains()
        {
            return TrainDatas;
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
            return new TrainData(train.TrainID, train.TrunkLine, train.Type, false, train.RunInfos, train.Carbins);
        }

        public IEnumerable<TrainData> GetTrains()
        {
            return dbContainer.Values;
        }

        public void RemoveTrain(string trainID)
        {
            dbContainer.Remove(trainID);
        }


    }
}
