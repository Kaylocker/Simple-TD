using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCharacterMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action<InformationPanel> OnCreatedPanel;

    [SerializeField] SpriteRenderer _informationPanel;
    [SerializeField] GameObject _buildingPrefab;

    private InformationPanel _activePanel;
    private Vector3 _panelPosition, _menuPosition;
    private Vector3 _panelPositionOffset, _additiontalOffSet = Vector3.zero;

    private void OnEnable()
    {
        _panelPosition = transform.position;
    }

    public void SetOffsetPositionInfoPanel(Vector3 offset)
    {
        _panelPositionOffset = offset;
        _panelPositionOffset += _additiontalOffSet;

        if (_menuPosition.y > 0)
        {
            _panelPositionOffset = -_panelPositionOffset;
        }
    }

    public void SetMenuPosition(Vector3 menuPosition)
    {
        _menuPosition = menuPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_activePanel == null)
        {
            CreateInformationPanel();
        }
        else
        {
            _activePanel.gameObject.SetActive(true);
        }
    }

    private void CreateInformationPanel()
    {
        GameObject activePanel = Instantiate(_informationPanel.gameObject, new Vector3(_panelPosition.x, _panelPosition.y + _panelPositionOffset.y, _panelPosition.z), Quaternion.identity);
        _activePanel = activePanel.GetComponent<InformationPanel>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_activePanel != null)
        {
            _activePanel.gameObject.SetActive(false);
        }
    }

    public void OnClickOnBuyCharacterButton()
    {
        if (_activePanel != null)
        {
            OnCreatedPanel?.Invoke(_activePanel);
            _activePanel.OnUpgradedCharacterLevel();
            _activePanel.gameObject.SetActive(false);
        }
    }

    public void OnClickedOnUpgradeButton()
    {
        //_activePanel.OnUpgradedCharacterLevel();
        _activePanel.gameObject.SetActive(false);
    }

    public void OnClickedOnSellCharacterButton()
    {
        Destroy(_activePanel.gameObject);
    }

    public void SetActiveInformationPanel(InformationPanel informationPanel)
    {
        _activePanel = informationPanel;
    }

    public InformationPanel GetActiveInformationPanel()
    {
        return _activePanel;
    }
}
