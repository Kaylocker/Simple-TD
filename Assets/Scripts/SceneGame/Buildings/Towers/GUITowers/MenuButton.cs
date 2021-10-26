using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] SpriteRenderer _informationPanel;
    [SerializeField] GameObject _buildingPrefab;

    private Building _building;
    private InformationPanelManager _activePanel;
    private Vector3 _panelPosition;
    private float _yOffSet, _additiontalOffSet;
    private bool _isNeedAdditiontalOffset;

    private void OnEnable()
    {
        if (_building != null)
        {
            _building = _buildingPrefab.GetComponent<Building>();
        }
    }


    public void SetMenuPosition(Vector3 positionOffSet, Vector3 basePosition)
    {
        _panelPosition = positionOffSet;

        if (_isNeedAdditiontalOffset)
            _additiontalOffSet = GetComponent<Image>().sprite.bounds.size.x;

        if (basePosition.y > 0)
        {
            _yOffSet = -_informationPanel.bounds.size.y / 2 - _additiontalOffSet;
        }
        else
        {
            _yOffSet = _informationPanel.bounds.size.y / 2 + _additiontalOffSet;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CreateInformationPanel();
    }

    private void CreateInformationPanel()
    {
        GameObject activePanel = Instantiate(_informationPanel.gameObject, new Vector3(_panelPosition.x, _panelPosition.y + _yOffSet, _panelPosition.z), Quaternion.identity);
        _activePanel = activePanel.GetComponent<InformationPanelManager>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_activePanel != null)
        {
            Destroy(_activePanel.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_activePanel != null)
        {
            Destroy(_activePanel.gameObject);
        }
    }

    public void SetAdditiontalOffsetFlag(bool status)
    {
        _isNeedAdditiontalOffset = status;
    }
}
