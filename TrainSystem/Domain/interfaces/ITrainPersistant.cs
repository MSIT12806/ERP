namespace Train
{
    public interface ITrainPersistant
    {
        TrainData GetTrain(string trainID);
        IEnumerable<TrainData> GetTrains(DateTime startTime,DateTime endTime);
        IEnumerable<TrainData> GetTrainsByID(string trainID, DateTime dateTime);
        IEnumerable<TrainData> GetTrainsByStation(string stationName, DateTime dateTime);
        void AddTrain(TrainData train);
        void RemoveTrain(string trainID);
        void EditTrain(TrainData train);
    }
}