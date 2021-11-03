using UnityEngine;
using UnityEngine.Events;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private EventInt OnGoldChanged;
    [SerializeField] private EventInt OnWoodChanged;

    private int _goldTotal, woodTotal;
    private const int GOLD_MAX = 99999, WOOD_MAX = 99999;
    protected float _refundPersentage = 0.6f;

    public int GoldTotal { get => _goldTotal; }
    public int WoodTotal { get => woodTotal; }

    private void Start()
    {
        ChangeGold(256);
        ChangeWood(256);
    }
    
    public void ChangeGold(int value)
    {
        _goldTotal += value;

        if (_goldTotal < 0)
        {
            _goldTotal = 0;
        }
        else if (_goldTotal > GOLD_MAX)
        {
            _goldTotal = GOLD_MAX;
        }

        OnGoldChanged?.Invoke(_goldTotal);
    }

    public void ChangeWood(int value)
    {
        woodTotal += value;

        if (woodTotal < 0)
        {
            woodTotal = 0;
        }
        else if (woodTotal > WOOD_MAX)
        {
            woodTotal = WOOD_MAX;
        }

        OnWoodChanged?.Invoke(woodTotal);
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

    public void MakePurchase(BuildingData buildingData, Character building)
    {
        int level = building.Level;

        ChangeGold(-buildingData.Levels[level].GoldCost);
        ChangeWood(-buildingData.Levels[level].WoodCost);
    }

    public void TakeRefund(Character character)
    {
        int totalRefundGold = (int)(character.TotalGoldCost * _refundPersentage);
        int totalRefundWood = (int)(character.TotalWoodCost * _refundPersentage);

        ChangeGold(totalRefundGold);
        ChangeWood(totalRefundWood);
    }
}

[System.Serializable]
public class EventInt : UnityEvent<int> { }
