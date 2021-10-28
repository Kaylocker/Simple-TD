using UnityEngine;
using System.Collections;

public class ResourcesMiner : Building
{
    protected int _goldPerDelay, _woodPerDelay;
    protected float _timeBetweenMine;
    protected WaitForSeconds _delayMining;

    protected void Go(string dataPath)
    {
        _resources = FindObjectOfType<ResourcesManager>();
        SetCharacterData(dataPath);
        SetCharacteristics();
        OnUpgraded += SetCharacteristics;
        StartCoroutine(Mining());
    }


    private void OnDisable()
    {
        OnUpgraded -= SetCharacteristics;
    }

    protected void SetCharacteristics()
    {
        _goldPerDelay = _data.Levels[_currentLevel].GoldPerTime;
        _woodPerDelay = _data.Levels[_currentLevel].WoodPerTime;
        _timeBetweenMine = _data.Levels[_currentLevel].ReloadTime;
        _delayMining = new WaitForSeconds(_timeBetweenMine);
    }

    protected IEnumerator Mining()
    {
        do
        {
            _resources.ChangeGold(_goldPerDelay);
            _resources.ChangeWood(_woodPerDelay);

            yield return _delayMining;

        } while (this.gameObject!=null);
    }
}
