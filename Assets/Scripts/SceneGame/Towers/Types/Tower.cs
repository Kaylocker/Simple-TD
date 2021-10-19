using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected ResourcesManager _resourcesManager;

    protected Projectile _projectile;
    protected TowerLevelsData _towerData;

    protected GameObject _activeRadius, _activeProjectile;

    protected SpriteRenderer _activeRadiusSprite;

    protected WaitForSeconds _delayFoundingEnemies = new WaitForSeconds(T_DELAY_FOUNDING_ENEMIES);
    protected WaitForSeconds _delayReload;

    protected Enemy _target;
    protected Vector3 _radiusTower;

    protected const float T_DELAY_FOUNDING_ENEMIES = 0.3f;
    protected float _timeDelayReload, _radius, _damageMin, _damageMax;

    protected int _level = 0;
    protected bool _isEnemyFinded, _isSelected;

    public SpriteRenderer ActiveRadius
    {
        get => _activeRadiusSprite;
    }

    protected IEnumerator FindEnemy()
    {
        yield return _delayFoundingEnemies;

        while (_target == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out Enemy enemy) && !_isEnemyFinded)
                {
                    _isEnemyFinded = true;

                    _target = enemy;
                    StartCoroutine(Shooting());

                    yield return null;
                }
            }

            yield return _delayFoundingEnemies;
        }
    }

    protected IEnumerator Shooting()
    {
        while (_target != null)
        {
            _activeProjectile = Instantiate(_projectile.gameObject, transform.position, Quaternion.identity);
            Projectile activeProjectileComponent = _activeProjectile.GetComponent<Projectile>();
            activeProjectileComponent.SetEnemy(_target);
            activeProjectileComponent.SetDamageRange(_damageMin, _damageMax);

            yield return _delayReload;

            CheckDistanceToEnemy();
        }
    }

    protected void CheckDistanceToEnemy()
    {
        if (_target == null)
        {
            _isEnemyFinded = false;
            StartCoroutine(FindEnemy());

            return;
        }

        Vector3 distance = _target.transform.position - transform.position;

        if (distance.magnitude > _radius)
        {
            _target = null;
            _isEnemyFinded = false;

            StartCoroutine(FindEnemy());
        }
    }

    public void UpgradeLevel()
    {
        if (_level >= _towerData.Levels.Count - 1)
        {
            _level = _towerData.Levels.Count - 1;

            return;
        }

        _level++;

        SetCharacteristic();
    }


    protected void InstantiateRangeRadius(SpriteRenderer circleRange)
    {
        _activeRadius = Instantiate(circleRange.gameObject, transform.position, Quaternion.identity);
        _activeRadius.SetActive(false);
        _activeRadiusSprite = _activeRadius.GetComponent<SpriteRenderer>();
        _activeRadiusSprite.transform.localScale += _radiusTower;

        _radius = _activeRadiusSprite.bounds.size.x / 2;
    }

    protected void SetTowerType(string path)
    {
        _towerData = GetCurrentTowerData(path);

        SetCharacteristic();
    }

    protected void SetCharacteristic()
    {
        _damageMin = _towerData.Levels[_level].DamageMin;
        _damageMax = _towerData.Levels[_level].DamageMax;
        _timeDelayReload = _towerData.Levels[_level].ReloadTime;
        _delayReload = new WaitForSeconds(_timeDelayReload);

        float sizeRadius = _towerData.Levels[_level].RadiusCoefficient;
        _radiusTower = new Vector3(sizeRadius, sizeRadius, 0);

        if (_level > 0)
        {
            _activeRadiusSprite.transform.localScale += _radiusTower;
            _radius = _activeRadiusSprite.bounds.size.x / 2;
        }
    }

    public TowerLevelsData GetCurrentTowerData(string path)
    {
        return _towerData = Resources.Load<TowerLevelsData>(path);
    }
}
