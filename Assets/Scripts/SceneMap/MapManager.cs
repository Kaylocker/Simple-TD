using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameEvent _loadLevel;

    LevelNameData _levelNameData = new LevelNameData();

    public void SetNewLevelName(string levelName)
    {
        _levelNameData.SetName(levelName);
        _loadLevel?.Call();
    }
}
