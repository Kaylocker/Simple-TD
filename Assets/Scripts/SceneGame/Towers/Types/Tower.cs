using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected GameObject _prefabRadiusRange, _activeRadius;
    protected SpriteRenderer _spriteRange;
    private const float _range = 1f;
    protected const float _delay = 0.5f;
    protected WaitForSeconds _waitForsecond = new WaitForSeconds(_delay);

    protected GameObject Range 
    {
        set
        {
            _prefabRadiusRange = value;
            _prefabRadiusRange.TryGetComponent(out _spriteRange);
        }
    }

    private void Start()
    {
        CreateRangeRadius();
    }

    protected virtual IEnumerator FindEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _spriteRange.bounds.size.x/2);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                Destroy(enemy.gameObject);
                print("hi");
            }
        }

        yield return _waitForsecond;

        StartCoroutine(FindEnemy());
    }
    
    private void CreateRangeRadius()
    {
        _activeRadius = Instantiate(_prefabRadiusRange, transform.position, Quaternion.identity);
        _activeRadius.SetActive(false);
    }

    private void OnMouseEnter()
    {
        _activeRadius.SetActive(true);
    }

    private void OnMouseExit()
    {
        _activeRadius.SetActive(false);
    }
}
