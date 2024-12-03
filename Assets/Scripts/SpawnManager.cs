using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab; // Enemy prefab
    [SerializeField] private Transform spawnArea;    // Reference to the spawn area
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(10f, 10f); // Size of the spawn area
    [SerializeField] private int maxEnemies = 5;    // Maximum number of enemies

    [Header("Spawn Timing")]
    [SerializeField] private float spawnInterval = 5.0f; // Time between spawn attempts
    private float spawnTimer;

    private List<GameObject> activeEnemies = new List<GameObject>(); // List to track spawned enemies

    private int enemyCount = 0; // Track number of enemies

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        // Check if enough time has passed and if the maximum number of enemies is not reached
        if (spawnTimer <= 0)
        {
            TrySpawnEnemies();
            spawnTimer = spawnInterval;
        }

        // Update the enemy count based on active enemies
        UpdateEnemyCount();

        // Debugging the count (for troubleshooting)
        //Debug.Log("Enemy Count: " + enemyCount);
    }

    private void TrySpawnEnemies()
    {
        // Only try to spawn enemies if the current count is less than the max
        if (enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        //set tag to enemy
        spawnedEnemy.tag = "enemy";

        // Add to the list and update the count only if the enemy is successfully spawned
        if (spawnedEnemy != null)
        {
            activeEnemies.Add(spawnedEnemy);
            enemyCount++; // Increase count when a new enemy is spawned
            Debug.Log("Spawned prey at position " + spawnPosition);
        }
    }

    private void UpdateEnemyCount()
    {
        // Enemy count is determined by the number of active enemies (it won't go below 0)
        enemyCount = activeEnemies.Count;

        if (enemyCount < 0) enemyCount = 0;
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
