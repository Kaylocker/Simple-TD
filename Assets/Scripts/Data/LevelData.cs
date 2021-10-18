using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "SimpleTD/Level")]
public class LevelData : ScriptableObject
{
    public List<Wave> Waves = new List<Wave>();
}

[System.Serializable]
public class Wave
{
    [Range(1f, 100f)]
    public float StartGold;
    [Range(1f, 100f)]
    public float StartWood;
    [Range(1f, 100f)]
    public int CountEnemies;
    [Range(1f,100f)]
    public int WaitToNextWave;
    public GameObject EnemyPrefabs;
}
