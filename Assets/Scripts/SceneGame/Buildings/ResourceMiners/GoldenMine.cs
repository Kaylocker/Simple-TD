using UnityEngine;

public class GoldenMine : ResourcesMiner, IBuildingData
{
    public string DataPath { get => "Miners/GoldenMiner"; }

    private void Awake()
    {
        _resources = FindObjectOfType<ResourcesManager>();
        SetBuildingType(DataPath);
        SetCharacteristics();
        StartCoroutine(Mining());
    }
}
