using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private GameObject _menuPrefab;
    [SerializeField] private TowerMenu _upgradeMenuPrefab;

    private Tower _tower;
    private SpriteRenderer _sprite;
    private TowerMenu _towerMenu;
    private Vector3 _positionMenu;
    private bool _isMenuActive, _isTowerPlaced;

    public bool IsTowerPlaced { get => _isTowerPlaced; }
    public bool Tower { get => _tower; }

    private void Start()
    {
        _positionMenu = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (_towerMenu != null && !_towerMenu.gameObject.activeInHierarchy)
        {
            _towerMenu.gameObject.SetActive(true);
            _isMenuActive = true;
            return;

        }

        if (_isMenuActive || _isTowerPlaced)
        {
            return;
        }

        _sprite.enabled = true;

        GenerateMenu();
    }

    private void OnMouseEnter()
    {
        if (_tower != null)
        {
            return;
        }

        _sprite.enabled = true;
    }

    private void OnMouseExit()
    {
        if (_isMenuActive)
        {
            return;
        }

        _sprite.enabled = false;
    }

    private void GenerateMenu()
    {
        GameObject menu = Instantiate(_menuPrefab, _positionMenu, Quaternion.identity);
        _towerMenu = menu.GetComponent<TowerMenu>();
        _towerMenu.BasePosition = transform.position;
        _towerMenu.BaseTower = this;
        _towerMenu.SetInformationTowerPanelsCenterPosition();
        _towerMenu.OnTowerBuilt += SetTowerOnBase;
        _towerMenu.OnDisableMenu += ActivateMenu;
        _isMenuActive = true;
    }

    private void SetTowerOnBase(Tower tower)
    {
        _isTowerPlaced = true;
        _tower = tower;
        tower.Base = this;
        tower.UpgradeMenu = _upgradeMenuPrefab;

        _towerMenu.OnTowerBuilt -= SetTowerOnBase;
        _towerMenu.OnDisableMenu -= ActivateMenu;

        _sprite.enabled = false;
        Destroy(_towerMenu.gameObject);
    }

    private void ActivateMenu()
    {
        _sprite.enabled = false;
        _isMenuActive = false;
    }
}
