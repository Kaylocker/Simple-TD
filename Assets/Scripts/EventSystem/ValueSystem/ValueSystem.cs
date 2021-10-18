using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class ValueSystem 
{
    [SerializeField] ValueEvent ValueChanged = new ValueEvent();

    private float _value;
    private float _maxValue;

    public void Setup(float value)
    {
        _value = _maxValue = value;

        ChangeValue();
    }

    public void AddValue(float value)
    {
        _value += value;

        if(_value > _maxValue)
        {
            _value = _maxValue;
        }

        ChangeValue();
    }

    public void RemoveValue(float value)
    {
        _value -= value;

        if (_value < 0)
        {
            _value = 0;
        }

        ChangeValue();
    }

    public void ChangeValue()
    {
        ValueChanged?.Invoke((float)_value/_maxValue);
    }

    [System.Serializable]

    public class ValueEvent : UnityEvent<float> { }
}
