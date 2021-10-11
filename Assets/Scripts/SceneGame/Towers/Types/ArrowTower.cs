using System.Collections;
using UnityEngine;

public class ArrowTower : Tower
{
    [SerializeField] private GameObject _circle;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    private void Awake()
    {
        Range = _circle;
        StartCoroutine(FindEnemy());
    }

}
