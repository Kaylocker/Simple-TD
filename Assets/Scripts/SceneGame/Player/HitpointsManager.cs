using UnityEngine;
using UnityEngine.Events;

public class HitpointsManager : MonoBehaviour
{
    [SerializeField] private int _hitpoints;

    [SerializeField] private EventIntInt OnChangeHitpoints;
    private int _maxHitpoints;

    private void Awake()
    {
        _maxHitpoints = _hitpoints;
        OnChangeHitpoints?.Invoke(_hitpoints, _maxHitpoints);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IEnemy enemy))
        {
            _hitpoints--;

            if (_hitpoints <= 0)
            {
                _hitpoints = 0;
            }

            OnChangeHitpoints?.Invoke(_hitpoints, _maxHitpoints);
        }
    }

    public void AddListeners(UnityAction<int, int> ChangeHitpoints)
    {
        OnChangeHitpoints.AddListener(ChangeHitpoints);
    }
    public void RemoveListeners(UnityAction<int, int> ChangeHitpoints)
    {
        OnChangeHitpoints.RemoveListener(ChangeHitpoints);
    }
}

[System.Serializable]
public class EventIntInt : UnityEvent<int, int> { }
