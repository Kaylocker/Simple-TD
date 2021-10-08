using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameEvent _loadLevel;
    LevelNameData _levelNameData = new LevelNameData();

    public void SetNewLevelName(string levelName)
    {
        _levelNameData.SetName(levelName);
        _loadLevel?.Call();
    }

}
