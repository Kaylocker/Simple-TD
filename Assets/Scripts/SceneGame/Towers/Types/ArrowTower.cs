using UnityEngine;

public class ArrowTower : Tower
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private SpriteRenderer _circleRange;

    private void Awake()
    {
        SetTowerType();
        InstantiateRangeRadius(_circleRange);

        _projectile = _projectilePrefab;

        StartCoroutine(FindEnemy());
    }

    private void SetTowerType()
    {
        _towerLevel = Resources.Load<TowerLevelsData>($"Towers/ArrowTower");

        SetCharacteristic();
    }
}
