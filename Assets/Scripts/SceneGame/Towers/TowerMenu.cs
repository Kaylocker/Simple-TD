using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    private Vector3 _basePosition;
    public Vector3 BasePosition
    {
        set
        {
            _basePosition = value;

        }
    }

    public void BuildTower(Tower tower)
    {
        print(_basePosition);
        Instantiate(tower.gameObject, _basePosition, Quaternion.identity);
    }
}
