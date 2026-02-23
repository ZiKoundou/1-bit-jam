using UnityEngine;

public class ZoomEffectManager : MonoBehaviour
{
    [SerializeField] private ZoomEffect zoomEffect;  // Drag your FlashEffect here

    void OnEnable()
    {
        TimePowerUp.OnPowerupCollected += OnPowerupPickedUp;
    }

    void OnDisable()
    {
        TimePowerUp.OnPowerupCollected -= OnPowerupPickedUp;
    }

    void OnPowerupPickedUp(Transform spawnPoint)
    {
        zoomEffect?.TriggerFlash();
        // Optional: Play sound, camera shake, score popup
    }
}