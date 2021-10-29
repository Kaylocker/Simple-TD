using UnityEngine;

public class MageBall : Projectile
{
    [SerializeField] private float _speed;

    private void Awake()
    {
        Speed = _speed;
    }
}
