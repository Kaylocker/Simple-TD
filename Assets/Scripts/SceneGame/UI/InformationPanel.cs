using UnityEngine;
using System;

public class InformationPanel : MonoBehaviour
{
    public event Action OnUpgradedCharacter;
    protected Character [] _characters;
    protected ICharacterData _interfaceCharacterData;
    protected int _level, _maxLevel;

    protected void Start()
    {
        FindActiveCharacters();

        if (_characters.Length!=0)
        {
            FindSelectedCharacter();
        }
    }

    protected void FindActiveCharacters()
    {
        _characters = FindObjectsOfType<Character>();
    }

    protected void FindSelectedCharacter()
    {
        foreach (var item in _characters)
        {
            if (item.IsSelected)
            {
                _interfaceCharacterData = item.GetComponent<ICharacterData>();
            }
        }
    }

    protected T GetData<T>() where T: ScriptableObject
    {
        if (_interfaceCharacterData == null)
        {
            return null;
        }
        else
        {
            T data = _interfaceCharacterData.GetCharacterData<T>();
            _level = _interfaceCharacterData.Level + 1;
            return data;
        }
    }

    protected void GetData(ref BuildingData data)
    {
        if(_interfaceCharacterData == null)
        {
            data = null;
        }
        else
        {
            BuildingData buildingData = _interfaceCharacterData.GetCharacterData<BuildingData>();
            _level = _interfaceCharacterData.Level + 1;
            data = buildingData;
        }
    }

    protected BuildingData GetDataFromPrefab<T>(T miner) where T: Building 
    {
        IBuildingData iData = miner.GetComponent<IBuildingData>();
        BuildingData data = iData.GetData(iData.DataPath);
        return data;
    }

    public void UpgradedCharacterLevel()
    {
        _level++;

        if (_level >= _maxLevel)
        {
            _level = _maxLevel;
        }
        
        OnUpgradedCharacter?.Invoke();
    }
}
