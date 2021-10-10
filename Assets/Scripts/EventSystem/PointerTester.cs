using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTester : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer is Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer is Up");
    }
}
