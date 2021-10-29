using UnityEngine;
using TMPro;

public class HitpointsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _hitpoints;

    private HitpointsManager _hitpointsManager;

    private void OnEnable()
    {
        if (_hitpointsManager == null)
        {
            _hitpointsManager = FindObjectOfType<HitpointsManager>();
            _hitpointsManager.AddListeners(ChangeHitpoints);
        }
    }

    private void OnDisable()
    {
        _hitpointsManager.RemoveListeners(ChangeHitpoints);
    }

    public void ChangeHitpoints(int currentHitpoints, int maxHitpoints)
    {
        _hitpoints.text = (currentHitpoints + "/" + maxHitpoints).ToString();
    }

}
