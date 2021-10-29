using UnityEngine;
using TMPro;

public class WavesStageUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _wavesStage;

    private EnemySpawner _enemySpawner;

    private void OnEnable()
    {
        if (_enemySpawner == null)
        {
            _enemySpawner = FindObjectOfType<EnemySpawner>();
            _enemySpawner.AddListeners(ShowWavesStage);
        }
    }

    private void OnDisable()
    {
        _enemySpawner.RemoveListeners(ShowWavesStage);
    }

    private void ShowWavesStage(int currentWave, int summaryWaves)
    {
        _wavesStage.text = ((currentWave + 1) + "/" + summaryWaves).ToString();
    }
}
