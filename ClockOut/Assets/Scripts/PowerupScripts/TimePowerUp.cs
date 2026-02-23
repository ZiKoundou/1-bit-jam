using System;
using UnityEngine;

public class TimePowerUp : MonoBehaviour
{
    [SerializeField] private float amount = 1800f;

    // NEW: This power-up will remember which spawn point it came from
    private Transform mySpawnPoint;

    public static event Action<Transform> OnPowerupCollected;  // Changed to pass the spawn point

    // Called by the manager right after spawning
    public void SetSpawnPoint(Transform spawnPoint)
    {
        mySpawnPoint = spawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WorldTimer.Instance.AdvanceTimer(amount);

            // Tell the manager exactly which spawn point to free up
            OnPowerupCollected?.Invoke(mySpawnPoint);

            Destroy(gameObject);
        }
    }
}