using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 _targetPoisitionAtMomentIdentification;
    protected Enemy _target;
    protected float _damage;

    public float Speed { get; protected set; }
    public float DamageMin { get; protected set; }
    public float DamageMax { get; protected set; }

    protected void FixedUpdate()
    {
        if (_target == null)
        {
            return;
        }

        Motion();
    }

    protected void OnDisable()
    {
        if (_target != null)
            _target.OnKilled -= DestroyProjectile;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetTarget(Enemy enemy)
    {
        _target = enemy;
        _targetPoisitionAtMomentIdentification = _target.transform.position;
        enemy.OnKilled += DestroyProjectile;
    }

    public void SetDamageRange(float damageMin, float damageMax)
    {
        DamageMin = damageMin;
        DamageMax = damageMax;
        GenerateDamage();
    }

    protected virtual void Motion()
    {
        Vector3 direction = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Speed * Time.fixedDeltaTime);

        if (transform.position == _target.transform.position)
        {
            HittingTheTarget(_target);
        }
    }

    protected void HittingTheTarget(IEnemy enemy)
    {
        enemy.TakeDamage(_damage);
        Destroy(gameObject);
    }

    protected void HittingTheTarget(List<IEnemy> enemies)
    {
        foreach (var item in enemies)
        {
            item.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }

    protected void GenerateDamage()
    {
        _damage = Random.Range(DamageMin, DamageMax);
    }
}
