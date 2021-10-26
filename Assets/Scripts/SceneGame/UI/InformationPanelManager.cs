using UnityEngine;

public class InformationPanelManager : MonoBehaviour
{
    protected Character [] _characters;
    protected ICharacterData _interfaceCharacterData;

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
            return data;
        }
    }

    protected BuildingData GetDataFromPrefab(Tower tower)
    {
        IBuildingData iData = tower.GetComponent<IBuildingData>();
        BuildingData data = iData.GetData(iData.DataPath);
        return data;
    }
}
