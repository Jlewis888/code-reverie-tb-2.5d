namespace CodeReverie
{
    public interface IDataPersistence
    {
        void LoadData(string dataSlot);
        void SaveData(string dataSlot);
    }
}