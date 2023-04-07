namespace Train
{
    public interface ITrainPersistant
    {
        TrainData GetTrain(string trainID);
        IEnumerable<TrainData> GetTrainsByID(string trainID, DateOnly date);
        IEnumerable<TrainData> GetTrainsByTime(string startStation, string targetStation, DateOnly date, char scaleType, TimeOnly startTime, TimeOnly endTime);
        IEnumerable<TrainData> GetTrainsByStation(string stationName, DateOnly dateTime);
        void AddTrain(TrainData train);
        void RemoveTrain(string trainID);
        void EditTrain(TrainData train);
    }
}