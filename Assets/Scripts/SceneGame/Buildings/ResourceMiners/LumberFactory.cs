using UnityEngine;

public class LumberFactory : ResourcesMiner, IBuildingData
{
    public string DataPath { get => "Miners/LumberFactory"; }

    private void Awake()
    {
        _resources = FindObjectOfType<ResourcesManager>();
        SetBuildingType(DataPath);
        SetCharacteristics();
        StartCoroutine(Mining());
    }
}
