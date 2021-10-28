using UnityEngine;

public class ArrowTower : Tower, IBuildingData
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private SpriteRenderer _circleRange;

    public string DataPath { get => "Towers/Arrow"; }

    private void Awake()
    {
        _resources = FindObjectOfType<ResourcesManager>();
        SetCharacterData(DataPath);
        SetCharacteristics();
        InstantiateRangeRadius(_circleRange);
        _projectile = _projectilePrefab;
    }
}
