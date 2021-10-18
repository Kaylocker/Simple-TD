using UnityEngine;
using UnityEngine.EventSystems;

public class TowerMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] SpriteRenderer _informationPanel;

    private GameObject _activePanel;
    private Vector3 _panelPosition;
    private float _yOffSet;

    public void SetMenuPosition(Vector3 positionOffSet, Vector3 centerPosition)
    {
        _panelPosition = positionOffSet;

        if(centerPosition.y > 0)
        {
            _yOffSet = -_informationPanel.bounds.size.y / 2;
        }
        else
        {
            _yOffSet = _informationPanel.bounds.size.y / 2;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _activePanel = Instantiate(_informationPanel.gameObject, new Vector3(_panelPosition.x, _panelPosition.y + _yOffSet, _panelPosition.z), Quaternion.identity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_activePanel != null)
        {
            Destroy(_activePanel);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_activePanel != null)
        {
            Destroy(_activePanel);
        }
    }
}
