using UnityEngine;

public class CannonTower : Tower, IBuildingData
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private SpriteRenderer _circleRange;

    public string DataPath { get => "Towers/Cannon"; }

    private void Awake()
    {
        _resources = FindObjectOfType<ResourcesManager>();
        SetCharacterData(DataPath);
        InstantiateRangeRadius(_circleRange);
        _projectile = _projectilePrefab;
    }
}
