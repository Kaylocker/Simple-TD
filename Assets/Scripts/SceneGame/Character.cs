using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityAction OnSolded;
    public UnityAction OnUpgraded;

    protected ResourcesManager _resources;
    protected InformationPanel _informationPanel;
    protected BuildingData _data;
    protected ICharacterData _characterData;

    protected int _currentLevel = -1;
    protected int _totalGoldCost, _totalWoodCost;

    public bool IsSelected { get; protected set; }
    public int TotalGoldCost { get => _totalGoldCost; }
    public int TotalWoodCost { get => _totalWoodCost; }

    public int Level { get => _currentLevel; }

    private void OnEnable()
    {
        //_informationPanel.OnUpgradedCharacterLevel();
    }

    public void SetInformationPanel(InformationPanel informationPanel)
    {
        _informationPanel = informationPanel;
    }

    public InformationPanel GetInformationPanel()
    {
        return _informationPanel;
    }

    public virtual void UpgradeLevel()
    {
        if (_currentLevel >= _data.Levels.Count - 1)
        {
            _currentLevel = _data.Levels.Count - 1;

            return;
        }

        if (_informationPanel != null)
        {
            _informationPanel.OnUpgradedCharacterLevel();
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

    public void Sold()
    {
        OnSolded?.Invoke();
        _resources.TakeRefund(this);
    }

    protected void UpgradeTotalCost()
    {
        _totalGoldCost += _data.Levels[_currentLevel].GoldCost;
        _totalWoodCost += _data.Levels[_currentLevel].WoodCost;

        print(_totalGoldCost);
        print(_totalWoodCost);
    }

    public T GetCharacterData<T>() where T : ScriptableObject
    {
        return (T)(_data as ScriptableObject);
    }

    public void SetCharacterData<T>(T data) where T : ScriptableObject
    {
        _characterData = (ICharacterData)data;
    }
}
