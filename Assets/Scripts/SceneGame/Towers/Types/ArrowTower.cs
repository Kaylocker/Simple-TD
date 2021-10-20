using UnityEngine;

public class ArrowTower : Tower, ITowerType
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private SpriteRenderer _circleRange;

    public string DataPath { get => "Towers/Arrow"; }

    private void Awake()
    {
        SetTowerType(DataPath);
        InstantiateRangeRadius(_circleRange);

        _projectile = _projectilePrefab;
        _resources = FindObjectOfType<ResourcesManager>();

        StartCoroutine(FindEnemy());
    }
}
