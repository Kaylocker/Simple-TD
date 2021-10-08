using UnityEngine;
using UnityEngine.Events;

public class WaveStarter : MonoBehaviour
{
    [SerializeField] UnityEvent OnClick;

    private void OnMouseDown()
    {
        print("start");
        OnClick?.Invoke();
    }
}
