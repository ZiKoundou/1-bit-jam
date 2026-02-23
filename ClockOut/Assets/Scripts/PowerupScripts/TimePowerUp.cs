using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimePowerUp : MonoBehaviour
{
    [SerializeField] private float amount = 1800f;

    // NEW: This power-up will remember which spawn point it came from
    private Transform mySpawnPoint;
    [SerializeField] private GameObject collisionEffect;
    [SerializeField] GameObject FloatingTextPrefab;
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
            Instantiate(collisionEffect, gameObject.transform.position, Quaternion.identity);
            ShowFloatingText(amount/60);
            // Tell the manager exactly which spawn point to free up
            OnPowerupCollected?.Invoke(mySpawnPoint);
            
            Destroy(gameObject);
        }
    }
    void ShowFloatingText(float timeAmount){

        var go = Instantiate(FloatingTextPrefab,transform.position,Quaternion.identity);
        go.GetComponentInChildren<TextMeshProUGUI>().text = "+" + timeAmount.ToString() + "min(s)";
    }
}   