using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnerEvents : MonoBehaviour
{
    [SerializeField] protected EventIntInt OnChangedWave;
    [SerializeField] protected EventInt OnResetTimeToNextWave;

    public void AddListeners(UnityAction<int> SetNewTimeToWave)
    {
        OnResetTimeToNextWave.AddListener(SetNewTimeToWave);
    }

    public void AddListeners(UnityAction<int, int> WaveChange)
    {
        OnChangedWave.AddListener(WaveChange);
    }

    public void RemoveListeners(UnityAction<int> SetNewTimeToWave)
    {
        OnResetTimeToNextWave.RemoveListener(SetNewTimeToWave);
    }

    public void RemoveListeners(UnityAction<int, int> WaveChange)
    {
        OnChangedWave.RemoveListener(WaveChange);
    }
}
