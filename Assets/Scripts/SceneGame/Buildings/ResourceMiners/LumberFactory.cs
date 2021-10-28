
public class LumberFactory : ResourcesMiner, IBuildingData
{
    public string DataPath { get => "Miners/LumberFactory"; }

    private void Awake()
    {
        Go(DataPath);
    }
}
