
public interface ITowerType
{
    public string DataPath { get; }

    public TowerLevelsData GetCurrentTowerData(string path);
}
