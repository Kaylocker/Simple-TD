using UnityEngine;

public class CannonTower : Tower, ITowerType
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private SpriteRenderer _circleRange;

    public string DataPath { get => "Towers/Cannon"; }

    private void Awake()
    {
        SetTowerType(DataPath);
        InstantiateRangeRadius(_circleRange);

        _projectile = _projectilePrefab;

        _resourcesManager = FindObjectOfType<ResourcesManager>();

        StartCoroutine(FindEnemy());
    }
}
