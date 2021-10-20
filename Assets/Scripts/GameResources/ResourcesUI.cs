using UnityEngine;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] TextMeshProUGUI _woodText;

    private ResourcesManager _resources;

    private void OnEnable()
    {
        if (_resources == null)
        {
            _resources = FindObjectOfType<ResourcesManager>();
            _resources.AddListeners(ChangeGoldTotal, ChangeWoodTotal);
        }
    }

    private void OnDisable()
    {
        _resources.RemoveListeners(ChangeGoldTotal, ChangeWoodTotal);
    }

    public void ChangeGoldTotal(int gold)
    {
        _goldText.text = gold.ToString();
    }

    public void ChangeWoodTotal(int wood)
    {
        _woodText.text = wood.ToString();
    }
}
