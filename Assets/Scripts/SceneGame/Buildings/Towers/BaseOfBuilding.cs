using UnityEngine;

public class BaseOfBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _buyMenuPrefab;
    [SerializeField] private GameObject _upgradeMenuPrefab;

    private BuildingMenu _activeMenu;
    private SpriteRenderer _baseSprite;
    private Building _building;
    private Vector3 _positionMenu;
    private bool _isBuildingPlaced;

    private static bool _isSomeMenuActive;

    public bool IsTowerPlaced { get => _isBuildingPlaced; }
    public Building Building { get => _building; }

    public BuildingMenu ActiveMenu { get => _activeMenu; }
    private void Start()
    {
        _positionMenu = transform.position;
        _baseSprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (_activeMenu != null && !_activeMenu.gameObject.activeInHierarchy && !_isSomeMenuActive)
        {
            _activeMenu.gameObject.SetActive(true);
            _isSomeMenuActive = true;

            if (_isBuildingPlaced)
            {
                _building.OnSelected?.Invoke();
            }
            else
            {
                _baseSprite.enabled = true;
            }
            return;
        }

        if (_isSomeMenuActive)
        {
            return;
        }

        GenerateMenu();
    }

    private void OnMouseEnter()
    {
        if (_building != null || _isSomeMenuActive)
        {
            return;
        }

        _baseSprite.enabled = true;
    }

    private void OnMouseExit()
    {
        if (_isSomeMenuActive)
        {
            return;
        }

        _baseSprite.enabled = false;
    }

    private void GenerateMenu()
    {
        GameObject menu;

        if (!_isBuildingPlaced)
        {
            menu = Instantiate(_buyMenuPrefab, _positionMenu, Quaternion.identity);
            _activeMenu = menu.GetComponent<BuildingMenu>();
            _activeMenu.BasePosition = transform.position;
            _activeMenu.SetBuilding(_building);
            _activeMenu.OnBuilt += Build;
            _activeMenu.OnDisableMenu += DisactivatedMenu;
            _activeMenu.OnDisableMenu += DisableBaseBackLight;
        }
        else
        {
            menu = Instantiate(_upgradeMenuPrefab, _positionMenu, Quaternion.identity);
            _activeMenu = menu.GetComponent<BuildingMenu>();
            _activeMenu.BasePosition = transform.position;
            _activeMenu.SetBuilding(_building);
            _activeMenu.CheckIsCharacterHaveInformationPanel();
            _activeMenu.OnUpgraded += _building.UpgradeLevel;
            _activeMenu.OnSold += OnSoldedBuilding;
            _activeMenu.OnSold += _building.Sold;
            _activeMenu.OnDisableMenu += DisactivatedMenu;

            _building.OnSelected?.Invoke();
        }

        _isSomeMenuActive = true;
    }

    private void Build(Building building)
    {
        DisableBaseBackLight();

        _isSomeMenuActive = false;
        _isBuildingPlaced = true;
        _building = building;
        _activeMenu.OnBuilt -= Build;

        Destroy(_activeMenu.gameObject);
    }

    private void DisableBaseBackLight()
    {
        _baseSprite.enabled = false;
    }

    private void DisactivatedMenu()
    {
        _isSomeMenuActive = false;

        if(_building!= null)
        {
            _building.OnDeselected?.Invoke();
        }
    }

    private void OnSoldedBuilding()
    {
        _isBuildingPlaced = false;
        _isSomeMenuActive = false;

        Destroy(_activeMenu.gameObject);
    }
}
