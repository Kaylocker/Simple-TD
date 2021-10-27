using UnityEngine;
using System;

public class BuildingMenu : MonoBehaviour
{
    public event Action<Building> OnBuilt;
    public event Action OnUpgraded;
    public event Action OnSold;
    public event Action OnDisableMenu;

    [SerializeField] ButtonCharacterMenu[] _buttons;

    private Building _building;
    private BuildingData _buildingData;
    private ResourcesManager _resourcesManager;
    private SpriteRenderer _sprite;

    private Vector3 _basePosition;
    public Vector3 BasePosition
    {
        set
        {
            _basePosition = value;

        }
    }
    public Building ActiveBuilding { get => _building; }

    private void OnEnable()
    {
        DefinePositionOfInformationPanelOnMenu();
    }

    public void DefinePositionOfInformationPanelOnMenu()
    {
        foreach (var item in _buttons)
        {
            if (item != null)
            {
                _sprite = GetComponent<SpriteRenderer>();
                item.SetMenuPosition(transform.position);
                item.SetOffsetPositionInfoPanel(new Vector3(0, _sprite.bounds.size.y / 2, 0));
            }
        }
    }

    public void Build(GameObject tower)
    {
        bool isEnoughResources = CheckRequiredAmountResources(tower);

        if (!isEnoughResources)
        {
            return;
        }

        GameObject gameObjectBuilding = Instantiate(tower.gameObject, _basePosition, Quaternion.identity);
        _building = gameObjectBuilding.GetComponent<Building>();

        OnBuilt?.Invoke(_building);
    }

    private bool CheckRequiredAmountResources(GameObject tower)
    {
        tower.TryGetComponent(out IBuildingData currentType);

        _buildingData = currentType.GetData(currentType.DataPath);
        _resourcesManager = FindObjectOfType<ResourcesManager>();

        if (_resourcesManager.GoldTotal > _buildingData.Levels[0].GoldCost && _resourcesManager.WoodTotal > _buildingData.Levels[0].WoodCost)
        {
            return true;
        }

        return false;
    }

    public void SetBuilding(Building building)
    {
        if (_building == null)
        {
            _building = building;
        }
    }
    public void DisableMenu()
    {
        OnDisableMenu?.Invoke();
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        DisableMenu();

        OnUpgraded?.Invoke();
    }

    public void Sell()
    {
        OnSold?.Invoke();
    }
}
