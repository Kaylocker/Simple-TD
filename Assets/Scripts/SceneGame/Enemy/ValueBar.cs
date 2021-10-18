 using UnityEngine;

[System.Serializable]
public class ValueBar : MonoBehaviour
{
    [SerializeField] Transform _lineBar;

    public void SetValue(float value)
    {
        if (value < 0)
        {
            value = 0;
        }

        if(value > 1)
        {
            value = 1;
        }

        _lineBar.localScale = new Vector2(value, 1f);
    }
}