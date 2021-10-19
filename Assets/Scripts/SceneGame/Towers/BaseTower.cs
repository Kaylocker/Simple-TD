using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private GameObject _buyMenuPrefab;
    [SerializeField] private GameObject _upgradeMenuPrefab;

    private GameObject _menu;
    private TowerMenu _activeMenu;
    private Tower _tower;
    private SpriteRenderer _baseSprite;

    private Vector3 _positionMenu;
    private bool _isMenuActive, _isTowerPlaced;

    public bool IsTowerPlaced { get => _isTowerPlaced; }
    public bool Tower { get => _tower; }

    private void Start()
    {
        _positionMenu = transform.position;
        _baseSprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (_activeMenu != null && !_activeMenu.gameObject.activeInHierarchy)
        {
            _activeMenu.gameObject.SetActive(true);
            _isMenuActive = true;

            if (_isTowerPlaced)
            {
                _tower.ActiveRadius.gameObject.SetActive(true);
            }
            else
            {
                _baseSprite.enabled = true;
            }
            return;
        }

        if (_isMenuActive)
        {
            return;
        }

        GenerateMenu();
    }

    private void OnMouseEnter()
    {
        if (_tower != null)
        {
            return;
        }

        _baseSprite.enabled = true;
    }

    private void OnMouseExit()
    {
        if (_isMenuActive)
        {
            return;
        }

        _baseSprite.enabled = false;
    }

    private void GenerateMenu()
    {
        if (_isTowerPlaced)
        {
            _menu = Instantiate(_upgradeMenuPrefab, _positionMenu, Quaternion.identity);
            _activeMenu = _menu.GetComponent<TowerMenu>();

            _activeMenu.OnDisableMenu += DisableRadiusTower;
            _activeMenu.OnTowerUpgraded += _tower.UpgradeLevel;

            _activeMenu.SetInformationTowerPanelsCenterPosition(true);
            _tower.ActiveRadius.gameObject.SetActive(true);
        }
        else
        {
            _menu = Instantiate(_buyMenuPrefab, _positionMenu, Quaternion.identity);
            _activeMenu = _menu.GetComponent<TowerMenu>();

            _activeMenu.OnTowerBuilt += BuildTowerOnBase;
            _activeMenu.OnDisableMenu += DisableMenu;

            _activeMenu.SetInformationTowerPanelsCenterPosition();
        }


        _activeMenu.BasePosition = transform.position;

        _isMenuActive = true;
    }

    private void BuildTowerOnBase(Tower tower)
    {
        DisableMenu();

        _isTowerPlaced = true;
        _tower = tower;

        _activeMenu.OnTowerBuilt -= BuildTowerOnBase;
        _baseSprite.enabled = false;

        Destroy(_activeMenu.gameObject);
    }

    private void DisableMenu()
    {
        _baseSprite.enabled = false;
        _isMenuActive = false;
    }

    protected void DisableRadiusTower()
    {
        _tower.ActiveRadius.gameObject.SetActive(false);
    }
}
