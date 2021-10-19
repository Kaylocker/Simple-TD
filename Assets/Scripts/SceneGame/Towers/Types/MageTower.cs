using UnityEngine;

public class MageTower : Tower, ITowerType
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private SpriteRenderer _circleRange;

    public string DataPath { get => "Towers/Mage"; }

    private void Awake()
    {
        SetTowerType(DataPath);
        InstantiateRangeRadius(_circleRange);

        _projectile = _projectilePrefab;

        _resourcesManager = FindObjectOfType<ResourcesManager>();

        StartCoroutine(FindEnemy());
    }
}
