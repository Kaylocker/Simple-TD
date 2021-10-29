using UnityEngine;
using System.Collections;

public class ResourcesMiner : Building
{
    protected int _goldPerTime, _woodPerTime;
    protected float _timeBetweenMine;
    protected WaitForSeconds _delayMining;

    protected void SetSettings(string dataPath)
    {
        _resources = FindObjectOfType<ResourcesManager>();
        SetCharacterData(dataPath);
        OnUpgraded += SetCharacteristics;
        StartCoroutine(Mining());
    }


    private void OnDisable()
    {
        OnUpgraded -= SetCharacteristics;
    }

    protected void SetCharacteristics()
    {
        _goldPerTime = _data.Levels[_currentLevel].GoldPerTime;
        _woodPerTime = _data.Levels[_currentLevel].WoodPerTime;
        _timeBetweenMine = _data.Levels[_currentLevel].ReloadTime;
        _delayMining = new WaitForSeconds(_timeBetweenMine);
    }

    protected IEnumerator Mining()
    {
        do
        {
            _resources.ChangeGold(_goldPerTime);
            _resources.ChangeWood(_woodPerTime);

            yield return _delayMining;

        } while (this.gameObject!=null);
    }
}
