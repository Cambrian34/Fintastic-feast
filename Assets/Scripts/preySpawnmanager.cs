using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preySpawnmanager : MonoBehaviour
{

    [Header("Spawn Settings")]
    [SerializeField] private GameObject preyPrefab; // prey prefab
    [SerializeField] private Transform spawnArea;    // Reference to the spawn area
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(10f, 10f); // Size of the spawn area
    [SerializeField] private int maxPrey = 5;    // Maximum number of prey

    [Header("Spawn Timing")]
    [SerializeField] private float spawnInterval = 5.0f; // Time between spawn attempts
    private float spawnTimer;

    private List<GameObject> activePrey = new List<GameObject>(); // List to track spawned prey

    private int preyCount = 0; // Track number of prey

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        // Check if enough time has passed and if the maximum number of prey is not reached
        if (spawnTimer <= 0)
        {
            TrySpawnprey();
            spawnTimer = spawnInterval;
        }

        // Update the prey count based on active prey
        UpdatepreyCount();

        // Debugging the count (for troubleshooting)
        //Debug.Log("prey Count: " + preyCount);
    }

    private void TrySpawnprey()
    {
        // Only try to spawn prey if the current count is less than the max
        if (preyCount < maxPrey)
        {
            Spawnprey();
        }
    }

    private void Spawnprey()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedprey = Instantiate(preyPrefab, spawnPosition, Quaternion.identity);
        //set tag to prey
        spawnedprey.tag = "food";

        // Add to the list and update the count only if the prey is successfully spawned
        if (spawnedprey != null)
        {
            activePrey.Add(spawnedprey);
            preyCount++; // Increase count when a new prey is spawned
            Debug.Log("Spawned prey at position " + spawnPosition);
        }
    }

    private void UpdatepreyCount()
    {
        // Remove null entries (destroyed prey) from the list
        activePrey.RemoveAll(prey => prey == null);

        // prey count is determined by the number of active prey
        preyCount = activePrey.Count;
        Debug.Log("prey Count: " + preyCount);

        if (preyCount < 0) preyCount = 0;


    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);

        return new Vector2(spawnArea.position.x + randomX, spawnArea.position.y + randomY);
    }

    private void OnDrawGizmos()
    {
        // Draw the spawn area in the editor for visualization
        if (spawnArea == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnArea.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1));
    }
}
