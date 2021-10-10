using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TowerMenuButton : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
