using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private float _speed;

    private void Awake()
    {
        Speed = _speed;
    }
}
