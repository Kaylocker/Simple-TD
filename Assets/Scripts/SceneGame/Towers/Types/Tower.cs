using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected Enemy _target;
    protected ResourcesManager _resources;
    protected Projectile _projectile;
    protected TowerLevelsData _data;
    protected SpriteRenderer _activeRadius;

    protected WaitForSeconds _delaySearchingEnemies = new WaitForSeconds(DELAY_SEARCHING_ENEMIES);
    protected WaitForSeconds _delayReload;
  
    protected Vector3 _firingCircleZone;

    protected const float DELAY_SEARCHING_ENEMIES = 0.3f;

    protected float _timeDelayReload, _damageMin, _damageMax, _radiusFiring;
    protected int _level = 0;
    protected bool _isEnemyFinded;

    public SpriteRenderer ActiveRadius
    {
        get => _activeRadius;
    }

    protected IEnumerator FindEnemy()
    {
        yield return _delaySearchingEnemies;

        while (_target == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radiusFiring);

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

            yield return _delaySearchingEnemies;
        }
    }

    protected IEnumerator Shooting()
    {
        GameObject projectile;

        while (_target != null)
        {
            projectile = Instantiate(_projectile.gameObject, transform.position, Quaternion.identity);
            Projectile activeProjectileComponent = projectile.GetComponent<Projectile>();
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

        if (distance.magnitude > _radiusFiring)
        {
            _target = null;
            _isEnemyFinded = false;

            StartCoroutine(FindEnemy());
        }
    }

    public void UpgradeLevel()
    {
        if (_level >= _data.Levels.Count - 1)
        {
            _level = _data.Levels.Count - 1;

            return;
        }

        _level++;

        SetCharacteristic();
    }

    protected void InstantiateRangeRadius(SpriteRenderer circleRange)
    {
        GameObject radius = Instantiate(circleRange.gameObject, transform.position, Quaternion.identity);
        radius.SetActive(false);
        _activeRadius = radius.GetComponent<SpriteRenderer>();

        SetRadiusSize();
    }

    private void SetRadiusSize()
    {
        _activeRadius.transform.localScale += _firingCircleZone;
        _radiusFiring = _activeRadius.bounds.size.x / 2;
    }

    protected void SetTowerType(string path)
    {
        _data = GetCurrentTowerData(path);

        SetCharacteristic();
    }

    protected void SetCharacteristic()
    {
        _damageMin = _data.Levels[_level].DamageMin;
        _damageMax = _data.Levels[_level].DamageMax;
        _timeDelayReload = _data.Levels[_level].ReloadTime;
        _delayReload = new WaitForSeconds(_timeDelayReload);

        float sizeRadius = _data.Levels[_level].RadiusFiring;
        _firingCircleZone = new Vector3(sizeRadius, sizeRadius, 0);

        if (_level > 0)
        {
            SetRadiusSize();
        }
    }

    public TowerLevelsData GetCurrentTowerData(string path)
    {
        return _data = Resources.Load<TowerLevelsData>(path);
    }
}
