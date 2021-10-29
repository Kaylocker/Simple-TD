using UnityEngine;

public interface IEnemy : IDamagable
{
    void MovementLogic();
    float Speed { get; }
    float Health { get; }
    Vector3 Position { get; }
}
