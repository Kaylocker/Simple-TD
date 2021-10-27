using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinerInfoPanel : InformationPanelManager
{
    [SerializeField] private TextMeshProUGUI _resourcersPerTime;
    [Space]
    [SerializeField] private TextMeshProUGUI _delayBetweenFarm;
    [Space]
    [SerializeField] private TextMeshProUGUI _goldCost;
    [Space]
    [SerializeField] private TextMeshProUGUI _woodCost;
    [Space]
    [SerializeField] private ResourcesMiner _miner;

    private BuildingData _buildingData;

    private new void Start()
    {
        base.Start();

        _buildingData = GetData<BuildingData>();

        if (_buildingData == null)
        {
            _buildingData = GetDataFromPrefab(_miner);
        }

        SetTextInfoOfCharacteristics();
    }

    private void SetTextInfoOfCharacteristics()
    {
        _resourcersPerTime.text = (_buildingData.Levels[_level].DamageMin + " - " + _buildingData.Levels[_level].DamageMax).ToString();
        _delayBetweenFarm.text = _buildingData.Levels[_level].ReloadTime.ToString();
        _goldCost.text = _buildingData.Levels[_level].GoldCost.ToString();
        _woodCost.text = _buildingData.Levels[_level].WoodCost.ToString();
    }
}
