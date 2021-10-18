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

    private SpriteRenderer _sprite;
    private BaseTower _baseTower = null;
    private Tower _tower;

    private Vector3 _basePosition;

    public Vector3 BasePosition
    {
        set
        {
            _basePosition = value;

        }
    }

    public BaseTower BaseTower
    {
        set
        {
            if (_baseTower == null)
            {
                _baseTower = value;
            }
        }
    }

    public Tower Tower
    {
        get => _tower;

        set
        {
            if (_tower == null)
            {
                _tower = value;
            }
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
            if(item != null)
            {
                item.SetMenuPosition(new Vector3(transform.position.x, (transform.position.y + yOffset), transform.position.z), _basePosition);
            }
        }
    }

    public void Build(Tower tower)
    {
        if (_baseTower.IsTowerPlaced || _baseTower == null)
        {
            return;
        }

        GameObject gmTower = Instantiate(tower.gameObject, _basePosition, Quaternion.identity);
        Tower activeTower = gmTower.GetComponent<Tower>();
        OnTowerBuilt?.Invoke(activeTower);
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
