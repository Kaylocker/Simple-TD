
public class GoldenMine : ResourcesMiner, IBuildingData
{
    public string DataPath { get => "Miners/GoldenMiner"; }

    private void Awake()
    {
        SetSettings(DataPath);
    }
}
