using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfiguration", menuName = "SimpleTD/TowerConfiguration")]
public class TowerLevelsData : ScriptableObject
{
    public List<Level> Levels = new List<Level>();
}

[System.Serializable]
public class Level
{
    [Range(1, 1000)]
    public int GoldCost;
    [Range(1, 1000)]
    public int WoodCost;
    [Range(1f, 100f)]
    [Space]
    public float DamageMin;
    [Range(1f, 100f)]
    public float DamageMax;
    [Range(0.1f, 2.0f)]
    public float ReloadTime;
    [Range(0.1f, 10.0f)]
    public float RadiusFiring;
}
