namespace Train
{
    public interface ITrainPersistant
    {
        TrainData GetTrain(string trainID);
    }
}