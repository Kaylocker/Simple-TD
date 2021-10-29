using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _wavesStage;

    private EnemySpawner _enemySpawner;
    private float _timeToWave;

    private void OnEnable()
    {
        if (_enemySpawner == null)
        {
            _enemySpawner = FindObjectOfType<EnemySpawner>();
            _enemySpawner.AddListeners(SetTimeTowave);
        }
    }

    private void OnDisable()
    {
        _enemySpawner.RemoveListeners(SetTimeTowave);
    }

    private void Update()
    {
        if (_timeToWave <= 0)
        {
            _wavesStage.text = "IN PROGRESS";
            return;
        }

        _timeToWave -= Time.deltaTime;
        _wavesStage.text = "Next wave in: " + Mathf.Round(_timeToWave).ToString();
    }

    private void SetTimeTowave(int timetoWave)
    {
        _timeToWave = timetoWave;
    }
}
