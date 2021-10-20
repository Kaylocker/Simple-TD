using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasisTower : Ground
{
    [SerializeField] private GameObject _buyMenuPrefab;
    [SerializeField] private GameObject _upgradeMenuPrefab;

    private SpriteRenderer _baseSprite;
    private TowerMenu _activeMenu;
    private Vector3 _positionMenu;

    private void Start()
    {
        _positionMenu = transform.position;
        _baseSprite = GetComponent<SpriteRenderer>();
    }

    //private void OnMouseDown()
    //{
    //    if (_activeMenu != null && !_activeMenu.gameObject.activeInHierarchy)
    //    {
    //        _activeMenu.gameObject.SetActive(true);
    //        _isMenuActive = true;

    //        if (_isBuildingPlaced)
    //        {
    //            _tower.ActiveRadius.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            _baseSprite.enabled = true;
    //        }
    //        return;
    //    }

    //    if (_isMenuActive)
    //    {
    //        return;
    //    }

    //    GenerateMenu();
    //}
}
