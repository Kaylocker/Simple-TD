using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private string _levelName;
    [SerializeField] private UnityEvent OnClick;

    private int _index;

    private void Start()
    {
        Int32.TryParse(_text.text, out _index);
    }

    private void OnMouseDown()
    {
        LevelNameData levelNameData = new LevelNameData();
        levelNameData.SetName(_levelName);
        levelNameData.SetLevelIndex(_index);
        OnClick?.Invoke();
    }
}
