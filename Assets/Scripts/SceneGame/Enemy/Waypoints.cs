using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    public Transform this[int index] { get=>_waypoints[index]; }
    public int Count { get => _waypoints.Length; }
}
