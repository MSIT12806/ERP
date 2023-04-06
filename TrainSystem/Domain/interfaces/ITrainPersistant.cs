namespace Train
{
    public interface ITrainPersistant
    {
        TrainData GetTrain(string trainID);
        IEnumerable<TrainData> GetTrains(DateTime startTime,DateTime endTime);
        IEnumerable<TrainData> GetTrainsByID(string trainID, DateTime dateTime);
        IEnumerable<TrainData> GetTrainsByStation(string stationName, DateTime dateTime);
        TrainData AddTrain(string trainID);
        TrainData RemoveTrain(string trainID);
        TrainData EditTrain(string trainID);
    }
}