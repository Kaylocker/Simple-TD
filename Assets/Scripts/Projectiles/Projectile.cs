using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy _enemy;
    private const float _speed = 5f;
    private float _damageMin, _damageMax;

    private void FixedUpdate()
    {
        if (_enemy == null)
        {
            return;
        }

        Motion();
    }

    private void OnDisable()
    {
        _enemy.OnKilled -= DestroyProjectile;
    }
    
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
        enemy.OnKilled += DestroyProjectile;
    }

    public void SetDamageRange(float damageMin, float damageMax)
    {
        _damageMin = damageMin;
        _damageMax = damageMax;
    }

    public void Motion()
    {
        Vector3 direction = _enemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.fixedDeltaTime);

        if (transform.position == _enemy.transform.position)
        {
            float damage = Random.Range(_damageMin, _damageMax);
            _enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
