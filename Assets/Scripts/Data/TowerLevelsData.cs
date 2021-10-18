using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerLevel", menuName = "SimpleTD/TowerLevel")]
public class TowerLevelsData : ScriptableObject
{
    public List<Level> Levels = new List<Level>();
}

[System.Serializable]
public class Level
{
    [Range(1f, 1000f)]
    public float Cost;
    [Range(1f, 100f)]
    public float DamageMin;
    [Range(1f, 100f)]
    public float DamageMax;
    [Range(0.1f, 2.0f)]
    public float ReloadTime;
    [Range(0.1f, 10.0f)]
    public float RadiusCoefficient;
}
