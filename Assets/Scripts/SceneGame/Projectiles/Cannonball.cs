using UnityEngine;
using System.Collections.Generic;

public class Cannonball : Projectile
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _rangeSplashAttack;

    private void Awake()
    {
        Speed = _speed;
    }

    protected override void Motion()
    {
        Vector3 direction = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, _targetPoisitionAtMomentIdentification, _speed * Time.fixedDeltaTime);

        if (transform.position == _targetPoisitionAtMomentIdentification)
        {
            List<IEnemy> enemies = new List<IEnemy>();
            FindAllEnemiesInRange(ref enemies);
            HittingTheTarget(enemies);
        }
    }

    private void FindAllEnemiesInRange(ref List<IEnemy> enemies)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _rangeSplashAttack);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out IEnemy enemy))
            {
                enemies.Add(enemy);
            }
        }
    }
        
}
