using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Waypoints _waypoints;

    private LevelData _levelData;
    private int _indexWave = 0;
    private const float DELAY_BETWEEN_ENEMIES_SPAWNING = 1f;

    private void Awake()
    {
        int currentLevel = new LevelNameData().GetLevelIndex();
        _levelData = Resources.Load<LevelData>($"Levels/Level{currentLevel}");
    }

    public void ActivateWave()
    {
        StartCoroutine(GenerateWave());
    }

    public IEnumerator GenerateWave()
    {
        WaitForSeconds delayBetweenSpawnEnemies = new WaitForSeconds(DELAY_BETWEEN_ENEMIES_SPAWNING);

        for (int j = 0; j < _levelData.Waves[_indexWave].CountEnemies - 1; j++)
        {
            GameObject enemy = Instantiate(_levelData.Waves[_indexWave].EnemyPrefabs, transform);
            Enemy _enemyComponent = enemy.GetComponent<Enemy>();
            _enemyComponent.SetWaypoints(_waypoints);

            yield return delayBetweenSpawnEnemies;
        }

        _indexWave++;

        if (_indexWave < _levelData.Waves.Count)
        {
            Invoke("ActivateWave", _levelData.Waves[_indexWave].WaitToNextWave);
        }
    }
}
