using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TowerMenuButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject _informationPanel;
    public UnityEvent OnClick;


    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    private void OnMouseEnter()
    {
        _informationPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        _informationPanel.SetActive(false);
    }
}
