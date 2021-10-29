using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityAction OnSolded;
    public UnityAction OnUpgraded;

    protected ResourcesManager _resources;
    protected InformationPanel _informationPanel;
    protected ICharacterData _characterData;

    protected int _currentLevel = -1, _maxLevel;
    protected int _totalGoldCost, _totalWoodCost;

    public bool IsSelected { get; protected set; }
    public int TotalGoldCost { get => _totalGoldCost; }
    public int TotalWoodCost { get => _totalWoodCost; }

    public int Level { get => _currentLevel; }

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
        if (_currentLevel >= _maxLevel)
        {
            _currentLevel = _maxLevel;

            return;
        }

        _currentLevel++;

        OnUpgraded?.Invoke();
    }

    public void SoldCharacter()
    {
        OnSolded?.Invoke();
        _resources.TakeRefund(this);
    }

    protected void SetMaxLevel(int maxLevel)
    {
        _maxLevel = maxLevel;
    }

    public void SetCharacterData<T>(T data) where T : ScriptableObject
    {
        _characterData = (ICharacterData)data;
    }

    public void SetData(IBuildingData buildingData)
    {
        _characterData = buildingData;
    }

    public T GetCharacterData<T>() where T : ScriptableObject
    {
        return (T)(_characterData as ScriptableObject);
    }

    public void DestroyCharacter()
    {
        Destroy(gameObject);
    }
}
