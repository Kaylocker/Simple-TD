using UnityEngine;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _goldAmountText;
    [SerializeField] TextMeshProUGUI _woodAmountText;

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
        _goldAmountText.text = gold.ToString();
    }

    public void ChangeWoodTotal(int wood)
    {
        _woodAmountText.text = wood.ToString();
    }
}
