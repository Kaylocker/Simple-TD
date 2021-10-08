using UnityEngine;

public class StartEvents : MonoBehaviour
{
    [SerializeField] GameEvent _startScene;
    [SerializeField] GameEvent _gameplay;

    private void Start()
    {
        _startScene.Call();
        _gameplay.Call();
    }
}
