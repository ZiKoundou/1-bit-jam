using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] private int maxPowerups = 3;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private List<Transform> availablePoints = new List<Transform>();
    private List<Transform> occupiedPoints = new List<Transform>();   // NEW

    private void OnEnable()  => TimePowerUp.OnPowerupCollected += FreeSpawnPoint;
    private void OnDisable() => TimePowerUp.OnPowerupCollected -= FreeSpawnPoint;

    void Start()
    {
        availablePoints.AddRange(spawnPoints);   // Start with all points available
    }

    void Update()
    {
        // Keep spawning until we hit the max
        while (occupiedPoints.Count < maxPowerups && availablePoints.Count > 0)
        {
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        if (availablePoints.Count == 0) return;

        // Pick random AVAILABLE point
        int randomIndex = Random.Range(0, availablePoints.Count);
        Transform chosenPoint = availablePoints[randomIndex];

        // Spawn the power-up
        GameObject newPowerup = Instantiate(powerUpPrefab, chosenPoint.position, Quaternion.identity);

        // Tell the power-up which spawn point it used
        newPowerup.GetComponent<TimePowerUp>()?.SetSpawnPoint(chosenPoint);

        // Move point from available → occupied
        availablePoints.RemoveAt(randomIndex);
        occupiedPoints.Add(chosenPoint);
    }

    // Called when player picks up a power-up
    void FreeSpawnPoint(Transform freedPoint)
    {
        if (freedPoint == null) return;

        // Remove from occupied
        occupiedPoints.Remove(freedPoint);

        // Add back to available (so it can spawn again later)
        if (!availablePoints.Contains(freedPoint))
            availablePoints.Add(freedPoint);
    }
}