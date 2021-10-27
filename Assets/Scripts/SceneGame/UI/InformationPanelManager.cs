using UnityEngine;

public class InformationPanelManager : MonoBehaviour
{
    protected Character [] _characters;
    protected ICharacterData _interfaceCharacterData;
    protected int _level;

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

    protected BuildingData GetDataFromPrefab<T>(T miner) where T: Building 
    {
        IBuildingData iData = miner.GetComponent<IBuildingData>();
        BuildingData data = iData.GetData(iData.DataPath);
        return data;
    }
}
