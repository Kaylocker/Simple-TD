using UnityEngine;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] TextMeshProUGUI _woodText;

    private ResourcesManager _resourcesManager;

    private void OnEnable()
    {
        if (_resourcesManager == null)
        {
            _resourcesManager = FindObjectOfType<ResourcesManager>();
            _resourcesManager.AddListeners(ChangeGoldTotal, ChangeWoodTotal);
        }
    }

    private void OnDisable()
    {
        _resourcesManager.RemoveListeners(ChangeGoldTotal, ChangeWoodTotal);
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
