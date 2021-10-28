using UnityEngine;
using TMPro;


public class TowerInfoPanel : InformationPanel
{
    [SerializeField] private TextMeshProUGUI _damage;
    [Space]
    [SerializeField] private TextMeshProUGUI _speed;
    [Space]
    [SerializeField] private TextMeshProUGUI _goldCost;
    [Space]
    [SerializeField] private TextMeshProUGUI _woodCost;
    [Space]
    [SerializeField] private Tower _tower;

    private BuildingData _buildingData;

    private new void Start()
    {
        base.Start();

        _buildingData = GetData<BuildingData>();

        if (_buildingData == null)
        {
            _buildingData = GetDataFromPrefab(_tower);
        }

        SetTextInfoOfCharacteristics();

        OnUpgradedCharacter += SetTextInfoOfCharacteristics;
    }

    private void SetTextInfoOfCharacteristics()
    {
        _damage.text = (_buildingData.Levels[_level].DamageMin + " - " + _buildingData.Levels[_level].DamageMax).ToString();
        _speed.text = _buildingData.Levels[_level].ReloadTime.ToString();
        _goldCost.text = _buildingData.Levels[_level].GoldCost.ToString();
        _woodCost.text = _buildingData.Levels[_level].WoodCost.ToString();
    }
}
