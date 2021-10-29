using System.Collections;
using UnityEngine;

public class Tower : Building
{
    protected Enemy _target;
    protected Projectile _projectile;
    protected SpriteRenderer _activeRadius;

    protected WaitForSeconds _delaySearchingEnemies = new WaitForSeconds(DELAY_SEARCHING_ENEMIES);
    protected WaitForSeconds _delayReload;

    protected Vector3 _firingCircleZone;

    protected const float DELAY_SEARCHING_ENEMIES = 0.3f;

    protected float _timeDelayReload, _damageMin, _damageMax, _radiusFiring;
    protected bool _isEnemyFinded;
    public SpriteRenderer ActiveRadius { get => _activeRadius; }
    
    private new void Start()
    {
        StartCoroutine(FindEnemy());
        base.Start();
    }

    private void OnEnable()
    {
        OnSelected += EnableRadiusTower;
        OnDeselected += DisableRadiusTower;
        OnSolded += DestroyRadius;
        OnUpgraded += SetCharacteristics;
    }

    private void OnDisable()
    {
        OnSelected -= EnableRadiusTower;
        OnDeselected -= DisableRadiusTower;
        OnSolded -= DestroyRadius;
        OnUpgraded -= SetCharacteristics;
    }

    protected IEnumerator FindEnemy()
    {
        while (_target == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radiusFiring);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out IEnemy enemy) && !_isEnemyFinded)
                {
                    _isEnemyFinded = true;

                    _target = enemy as Enemy;
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

            if(_target == null)
            {
                _isEnemyFinded = false;
                StartCoroutine(FindEnemy());

                yield return null;
            }
            else
            {
                activeProjectileComponent.SetTarget(_target);
                activeProjectileComponent.SetDamageRange(_damageMin, _damageMax);
            }

            yield return _delayReload;

            CheckDistanceToTarget();
        }
    }

    protected void CheckDistanceToTarget()
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

    protected void InstantiateRangeRadius(SpriteRenderer circleRange)
    {
        GameObject radius = Instantiate(circleRange.gameObject, transform.position, Quaternion.identity);
        radius.SetActive(false);
        _activeRadius = radius.GetComponent<SpriteRenderer>();

        SetRadiusSize();
    }

    private void SetRadiusSize()
    {
        _activeRadius.transform.localScale = _firingCircleZone;
        _radiusFiring = _activeRadius.bounds.size.x / 2;
    }

    protected void SetCharacteristics()
    {
        _damageMin = _data.Levels[_currentLevel].DamageMin;
        _damageMax = _data.Levels[_currentLevel].DamageMax;
        _timeDelayReload = _data.Levels[_currentLevel].ReloadTime;
        _delayReload = new WaitForSeconds(_timeDelayReload);

        float sizeRadius = _data.Levels[_currentLevel].RadiusFiring;
        _firingCircleZone = new Vector3(sizeRadius, sizeRadius, 0);

        if (_currentLevel >= 0)
        {
            SetRadiusSize();
        }
    }

    protected void DisableRadiusTower()
    {
        _activeRadius.gameObject.SetActive(false);
    }

    protected void EnableRadiusTower()
    {
        _activeRadius.gameObject.SetActive(true);
    }

    protected void DestroyRadius()
    {
        Destroy(_activeRadius.gameObject);
    }
}
