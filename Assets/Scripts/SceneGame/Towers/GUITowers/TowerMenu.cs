using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class TowerMenu : MonoBehaviour
{
    public event Action<Tower> OnTowerBuilt;
    public event Action OnDisableMenu;

    private BaseTower _baseTower = null;

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

    public void BuildTower(Tower tower)
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
}
