
public class GoldenMine : ResourcesMiner, IBuildingData
{
    public string DataPath { get => "Miners/GoldenMiner"; }

    private void Awake()
    {
        Go(DataPath);
    }
}
