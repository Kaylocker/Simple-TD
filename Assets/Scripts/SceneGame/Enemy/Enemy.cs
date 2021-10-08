using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Waypoints _waypoints = null;
    private int _index = 0;
    private float _speed = 1f;

    private void Update()
    {
        if (_waypoints == null)
        {
            return;
        }

        MovementLogic();
    }

    private void MovementLogic()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_index].position, _speed * Time.deltaTime);

        if(transform.position == _waypoints[_index].position)
        {
            if(_index == _waypoints.Count - 1)
            {
                Destroy(gameObject);
            }

            _index++;
        }

    }

    public void SetWaypoints(Waypoints waypoints)
    {
        _waypoints = waypoints;
    }
}
