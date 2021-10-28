using UnityEngine;
using UnityEngine.Events;

public class Building : Character, ICharacterData
{
    public UnityAction OnSelected;
    public UnityAction OnDeselected;

    protected BuildingData _data;

    public BuildingData Data { get => _data; }

    protected void Start()
    {
        OnSelected += ActivateSelectedStatus;
        OnDeselected += DisactivateSelectedStatus;
    }

    private void OnDisable()
    {
        OnSelected -= ActivateSelectedStatus;
        OnDeselected -= DisactivateSelectedStatus;
    }

    public BuildingData GetData(string path)
    {
        return _data = Resources.Load<BuildingData>(path);
    }

    private void ActivateSelectedStatus()
    {
        IsSelected = true;
        print(IsSelected);
    }

    private void DisactivateSelectedStatus()
    {
        IsSelected = false;
        print(IsSelected);
    }

    protected void SetCharacterData(string path)
    {
        _data = GetData(path);
        UpgradeLevel();
    }
}
