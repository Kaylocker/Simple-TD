using UnityEngine;
using System;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public event Action<Tower> OnTowerBuilt;
    public event Action OnTowerUpgraded;
    public event Action OnTowerSold;
    public event Action OnDisableMenu;

    [SerializeField] TowerMenuButton[] _buttons;

    private TowerLevelsData _towerLevelsData;
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

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void SetInformationTowerPanelsCenterPosition(bool isNeedButtonOffset = false)
    {
        float yOffset = 0, buttonOffset = 0;

        if (isNeedButtonOffset)
        {
            Image image = _buttons[0].GetComponent<Image>();

            buttonOffset = image.sprite.bounds.size.y;
        }

        if (_basePosition.y > 0)
        {
            yOffset = -_sprite.bounds.size.y / 2 - buttonOffset;
        }
        else
        {
            yOffset = _sprite.bounds.size.y / 2 + buttonOffset;
        }

        foreach (var item in _buttons)
        {
            if (item != null)
            {
                item.SetMenuPosition(new Vector3(transform.position.x, (transform.position.y + yOffset), transform.position.z), _basePosition);
            }
        }
    }

    public void Build(Tower tower)
    {
        bool isEnoughResources = CheckRequiredAmountResources(tower);

        if (!isEnoughResources)
        {
            return;
        }

        GameObject gmTower = Instantiate(tower.gameObject, _basePosition, Quaternion.identity);
        Tower activeTower = gmTower.GetComponent<Tower>();


        _resourcesManager.ChangeGold(-_towerLevelsData.Levels[0].GoldCost);
        _resourcesManager.ChangeWood(-_towerLevelsData.Levels[0].WoodCost);

        OnTowerBuilt?.Invoke(activeTower);
    }

    private bool CheckRequiredAmountResources(Tower tower)
    {
        tower.TryGetComponent(out ITowerType currentType);

        _towerLevelsData = currentType.GetCurrentTowerData(currentType.DataPath);

         _resourcesManager = FindObjectOfType<ResourcesManager>();

        if (_resourcesManager.Gold > _towerLevelsData.Levels[0].GoldCost && _resourcesManager.Wood > _towerLevelsData.Levels[0].WoodCost)
        {
            return true;
        }

        return false;
    }

    public void DisableMenu()
    {
        OnDisableMenu?.Invoke();
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        DisableMenu();

        OnTowerUpgraded?.Invoke();
    }

    public void Sell()
    {

    }
}
