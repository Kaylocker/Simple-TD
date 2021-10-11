using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private GameObject _menuPrefab;
    [SerializeField] private List<TowerSideTrigger> _triggers;

    private Tower _tower;
    private SpriteRenderer _sprite;
    private BoxCollider2D _baseCollider;
    private TowerMenu _towerMenu;
    private Vector3 _positionMenu;
    private bool _isMenuActive, _isTowerPlaced;
    private const int LEFT = 0, UP = 1, RIGHT = 2, DOWN = 3;

    public bool IsTowerPlaced { get => _isTowerPlaced; }

    private void Start()
    {
        _positionMenu = transform.position;
        _baseCollider = GetComponent<BoxCollider2D>();
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

        FindMenuPosition();
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

    private void FindMenuPosition()
    {
        int counter = 0;
        bool isFounded = false;

        foreach (var item in _triggers)
        {
            if (!item.IsCollided && !isFounded)
            {
                isFounded = true;

                switch (counter)
                {
                    case LEFT:
                        {
                            _positionMenu.x -= (_baseCollider.bounds.size.x);
                            return;
                        }
                    case UP:
                        {
                            _positionMenu.y += (_baseCollider.bounds.size.y);
                            return;
                        }
                    case RIGHT:
                        {
                            _positionMenu.x += (_baseCollider.bounds.size.x);
                            return;
                        }
                    case DOWN:
                        {
                            _positionMenu.y -= (_baseCollider.bounds.size.y);
                            return;
                        }
                    default:
                        break;
                }
            }

            counter++;
        }
    }

    private void GenerateMenu()
    {
        GameObject menu = Instantiate(_menuPrefab, _positionMenu, Quaternion.identity);
        _towerMenu = menu.GetComponent<TowerMenu>();
        _towerMenu.BasePosition = transform.position;
        _towerMenu.BaseTower = this;
        _towerMenu.OnTowerBuilt += SetOffPlace;
        _towerMenu.OnDisableMenu += ActivateMenu;
        _isMenuActive = true;
    }

    private void SetOffPlace(Tower tower)
    {
        _isTowerPlaced = true;
        _tower = tower;
        _towerMenu.OnTowerBuilt -= SetOffPlace;
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
