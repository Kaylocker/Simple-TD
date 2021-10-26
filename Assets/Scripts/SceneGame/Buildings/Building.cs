using UnityEngine;
using UnityEngine.Events;

public class Building : Character, ICharacterData
{
    public UnityAction OnSelected;
    public UnityAction OnDeselected;
    public UnityAction OnSolded;
    public UnityAction OnUpgraded;

    protected ResourcesManager _resources;
    protected BuildingData _data;
    protected int _currentLevel = -1;
    protected int _totalGoldCost, _totalWoodCost;

    public int TotalGoldCost { get => _totalGoldCost; }
    public int TotalWoodCost { get => _totalWoodCost; }

    public int Level { get => _currentLevel; }

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


    protected void SetBuildingType(string path)
    {
        _data = GetData(path);
        UpgradeLevel();
    }

    public virtual void UpgradeLevel()
    {
        if (_currentLevel >= _data.Levels.Count - 1)
        {
            _currentLevel = _data.Levels.Count - 1;
            return;
        }

        _currentLevel++;
        UpgradeTotalCost();

        OnUpgraded?.Invoke();

        MakePurchase();
    }

    private void MakePurchase()
    {
        _resources.MakePurchase(_data, this);
    }

    protected void UpgradeTotalCost()
    {
        _totalGoldCost += _data.Levels[_currentLevel].GoldCost;
        _totalWoodCost += _data.Levels[_currentLevel].WoodCost;

        print(_totalGoldCost);
        print(_totalWoodCost);
    }

    public void Sold()
    {
        OnSolded?.Invoke();
        _resources.TakeRefund(this);

        print(_totalGoldCost * 0.6);
        print(_totalWoodCost * 0.6);
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

    public T GetCharacterData<T>() where T : ScriptableObject
    {
        return (T)(_data as ScriptableObject);
    }
}
