using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingConfiguration", menuName = "SimpleTD/BuildingConfiguration")]
public class BuildingData : ScriptableObject
{
    public List<Level> Levels = new List<Level>();

}

[System.Serializable]
public class Level
{
    [Range(1, 10000)]
    public int GoldCost;
    [Range(1, 10000)]
    public int WoodCost;

    [Space]
    [Range(0f, 10000)]
    public int GoldPerTime;
    [Range(0f, 10000)]
    public int WoodPerTime;

    [Space]
    public float DamageMin;
    [Range(0.01f, 100.0f)]
    public float DamageMax;
    
    [Range(0.01f, 100.0f)]
    [Header("Reload time for an action depends on the type building")]
    public float ReloadTime;
    [Range(0.001f, 10.0f)]
    public float RadiusFiring;
}
