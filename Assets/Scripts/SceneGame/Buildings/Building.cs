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
        SubscribeOnEvents();
        SetMaxLevel(_data.Levels.Count-1);

        IBuildingData ibuilding = (IBuildingData)this;
        SetData(ibuilding);

        UpgradeLevel();
    }

    private void SubscribeOnEvents()
    {
        OnSelected += ActivateSelectedStatus;
        OnDeselected += DisactivateSelectedStatus;
        OnUpgraded += MakePurchase;
        OnUpgraded += _informationPanel.UpgradedCharacterLevel;
        OnSolded += DestroyCharacter;
    }

    private void OnDisable()
    {
        OnSelected -= ActivateSelectedStatus;
        OnDeselected -= DisactivateSelectedStatus;
        OnUpgraded -= MakePurchase;
        OnUpgraded -= _informationPanel.UpgradedCharacterLevel;
        OnSolded -= DestroyCharacter;
    }

    public BuildingData GetData(string path)
    {
        return _data = Resources.Load<BuildingData>(path);
    }

    private void ActivateSelectedStatus()
    {
        IsSelected = true;
    }

    private void DisactivateSelectedStatus()
    {
        IsSelected = false;
    }

    protected void SetCharacterData(string path)
    {
        _data = GetData(path);
    }

    private void MakePurchase()
    {
        _totalGoldCost += _data.Levels[_currentLevel].GoldCost;
        _totalWoodCost += _data.Levels[_currentLevel].WoodCost;
        print(_totalGoldCost);
        print(_totalWoodCost);
        _resources.MakePurchase(_data, this);
    }
}
