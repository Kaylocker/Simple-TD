using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private List<TowerSideTrigger> _triggers;

    private BoxCollider2D _baseCollider;
    private TowerMenu _towerMenu;
    private Vector3 _positionMenu;
    private float _yOffSet;
    private bool _isMenuActive, _isMenuActivated;
    private const int LEFT = 0, UP = 1, RIGHT = 2, DOWN = 3;


    private void Start()
    {
        _positionMenu = transform.position;
        _baseCollider = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        if(_isMenuActivated)
        {
            return;
        }    

        FindMenuPosition();
        GameObject menu = Instantiate(_menu, _positionMenu, Quaternion.identity);
        _towerMenu = menu.GetComponent<TowerMenu>();
        _towerMenu.BasePosition = transform.position;
        _isMenuActivated = true;
        _isMenuActive = true;
    }

    private void FindMenuPosition()
    {
        int counter = 0;
        bool isFounded = false;

        print("qq");

        foreach (var item in _triggers)
        {
            if (!item.IsCollided && !isFounded)
            {
                isFounded = true;

                switch (counter)
                {
                    case LEFT:
                        {
                            _positionMenu.x -= (_baseCollider.bounds.size.x / 2 + _baseCollider.bounds.size.x / 2);
                            return;
                        }
                    case UP:
                        {
                            _positionMenu.y += (_baseCollider.bounds.size.y / 2 + _baseCollider.bounds.size.y / 2);
                            return;
                        }
                    case RIGHT:
                        {
                            _positionMenu.x += (_baseCollider.bounds.size.x / 2 + _baseCollider.bounds.size.x / 2);
                            return;
                        }
                    case DOWN:
                        {
                            _positionMenu.y -= (_baseCollider.bounds.size.y / 2 + _baseCollider.bounds.size.y / 2);
                            return;
                        }
                    default:
                        break;
                }
            }

            counter++;
        }
    }
}
