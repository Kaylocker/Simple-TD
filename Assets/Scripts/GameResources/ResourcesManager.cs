using UnityEngine;
using UnityEngine.Events;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private EventInt OnGoldChanged;
    [SerializeField] private EventInt OnWoodChanged;

    private int _gold, _wood;
    private const int GOLD_MAX = 99999, WOOD_MAX = 99999;

    public int Gold { get => _gold; }
    public int Wood { get => _wood; }

    private void Awake()
    {
        ChangeGold(2500);
        ChangeWood(2500);
    }
    
    public void ChangeGold(int value)
    {
        _gold += value;

        if (_gold < 0)
        {
            _gold = 0;
        }
        else if (_gold > GOLD_MAX)
        {
            _gold = GOLD_MAX;
        }

        OnGoldChanged?.Invoke(_gold);
    }

    public void ChangeWood(int value)
    {
        _wood += value;

        if (_wood < 0)
        {
            _wood = 0;
        }
        else if (_wood > WOOD_MAX)
        {
            _wood = WOOD_MAX;
        }

        OnWoodChanged?.Invoke(_wood);
    }

    public void AddListeners(UnityAction<int> GoldChange, UnityAction<int> WoodChange)
    {
        OnGoldChanged.AddListener(GoldChange);
        OnWoodChanged.AddListener(WoodChange);
    }
    public void RemoveListeners(UnityAction<int> GoldChange, UnityAction<int> WoodChange)
    {
        OnGoldChanged.RemoveListener(GoldChange);
        OnWoodChanged.RemoveListener(WoodChange);
    }

}

[System.Serializable]
public class EventInt : UnityEvent<int> { }
