
public interface IBuildingData : ICharacterData
{
    public string DataPath { get; }

    public BuildingData GetData(string path);
}
