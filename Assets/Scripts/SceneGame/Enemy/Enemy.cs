using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnKilled;

    [SerializeField] private ValueSystem _healthSystem = new ValueSystem();

    [SerializeField] private float _speed;
    [SerializeField] private float _health;

    private Waypoints _waypoints = null;

    private int _indexOfWaypoints = 0;

    private void Start()
    {
        _healthSystem.Setup(_health);
    }

    private void FixedUpdate()
    {
        if (_waypoints == null)
        {
            return;
        }

        MovementLogic();
    }

    private void MovementLogic()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_indexOfWaypoints].position, _speed * Time.fixedDeltaTime);

        if(transform.position == _waypoints[_indexOfWaypoints].position)
        {
            if(_indexOfWaypoints == _waypoints.Count - 1)
            {
                Destroy(gameObject);
            }

            _indexOfWaypoints++;
        }
    }

    public void TakeDamage(float damage)
    {
        OnTakeDamage?.Invoke();

        _health -= damage;
        _healthSystem.RemoveValue(damage);

        if (_health < 0)
        {
            _health = 0;
            OnKilled?.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetWaypoints(Waypoints waypoints)
    {
        _waypoints = waypoints;
    }
}
