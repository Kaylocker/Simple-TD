using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected Projectile _projectile;
    protected TowerLevelsData _towerLevel;

    protected GameObject _activeRadius, _activeProjectile, _activeUpgradeMenu;

    protected SpriteRenderer _activeRadiusSprite;
    protected TowerMenu _upgradeMenuPrefab;
    protected TowerMenu _upgradeMenu;

    protected WaitForSeconds _delayFoundingEnemies = new WaitForSeconds(T_DELAY_FOUNDING_ENEMIES);
    protected WaitForSeconds _delayReload;

    protected BaseTower _base;
    protected Enemy _target;
    protected Vector3 _radiusTower;

    protected const float T_DELAY_FOUNDING_ENEMIES = 0.3f;
    protected float _timeDelayReload, _radius;
    protected float _damageMin, _damageMax;

    protected int _level = 0;
    protected bool _isEnemyFinded, _isSelected;

    public BaseTower Base
    {
        get => _base;

        set
        {
            if (_base == null)
            {
                _base = value;
            }
        }
    }

    public TowerMenu UpgradeMenu
    {
        get => _upgradeMenuPrefab;

        set
        {
            if (_upgradeMenuPrefab == null)
            {
                _upgradeMenuPrefab = value;
            }
        }
    }

    private void OnMouseDown()
    {
        if (_activeUpgradeMenu != null && !_activeUpgradeMenu.gameObject.activeInHierarchy)
        {
            _activeUpgradeMenu.gameObject.SetActive(true);
            _isSelected = true;
            _activeRadius.SetActive(true);
            return;
        }

        if (_isSelected)
        {
            return;
        }

        ActivateMenu();
    }

    private void OnDisable()
    {
        _upgradeMenu.OnDisableMenu -= DisableRadiusTower;
        _upgradeMenu.OnTowerUpgraded -= UpgradeLevel;
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

    private void UpgradeLevel()
    {
        if (_level >= _towerLevel.Levels.Count - 1)
        {
            _level = _towerLevel.Levels.Count - 1;

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

    protected void SetCharacteristic()
    {
        _damageMin = _towerLevel.Levels[_level].DamageMin;
        _damageMax = _towerLevel.Levels[_level].DamageMax;
        _timeDelayReload = _towerLevel.Levels[_level].ReloadTime;
        _delayReload = new WaitForSeconds(_timeDelayReload);

        float sizeRadius = _towerLevel.Levels[_level].RadiusCoefficient;
        _radiusTower = new Vector3(sizeRadius, sizeRadius, 0);

        if(_level>0)
        {
            _activeRadiusSprite.transform.localScale += _radiusTower;
            _radius = _activeRadiusSprite.bounds.size.x / 2;
        }
    }

    protected void SetMenuSettings()
    {
        if (_upgradeMenu == null)
        {
            _upgradeMenu = _activeUpgradeMenu.GetComponent<TowerMenu>();
            _upgradeMenu.Tower = this;

            _upgradeMenu.OnDisableMenu += DisableRadiusTower;
            _upgradeMenu.OnTowerUpgraded += UpgradeLevel;
        }
    }

    private void ActivateMenu()
    {
        _activeUpgradeMenu = Instantiate(_upgradeMenuPrefab.gameObject, transform.position, Quaternion.identity);

        SetMenuSettings();

        _upgradeMenu.SetInformationTowerPanelsCenterPosition(true);
        _isSelected = true;
        _activeRadius.SetActive(true);
    }

    protected void DisableRadiusTower()
    {
        _isSelected = false;
        _activeRadius.SetActive(false);
    }
}
