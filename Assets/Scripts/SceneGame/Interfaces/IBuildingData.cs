
public interface IBuildingData
{
    public string DataPath { get; }

    public BuildingData GetData(string path);
}
