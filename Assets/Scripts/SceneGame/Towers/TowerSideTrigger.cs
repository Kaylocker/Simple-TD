using UnityEngine;

public class TowerSideTrigger : MonoBehaviour
{
    private bool _isCollided = false;
    public bool IsCollided { get => _isCollided; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isCollided = true;
    }
}
