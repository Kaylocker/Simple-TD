using UnityEngine;

public class Ground : MonoBehaviour
{
    protected bool _isMenuActive, _isBuildingPlaced;

    public bool IsTowerPlaced { get => _isBuildingPlaced; }
}
