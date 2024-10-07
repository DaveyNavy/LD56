using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Reference to the object you want to spawn
    public GameObject objectToSpawn;

    // Time between spawns
    public float spawnInterval = 0.5f;

    // Maximum number of objects to spawn
    public int maxSpawnCount = 20;

    private int currentSpawnCount = 0;

    private void Start()
    {
        // Check if the objectToSpawn is assigned
        if (objectToSpawn == null)
        {
            Debug.LogError("objectToSpawn is not assigned in the Inspector.");
            return;
        }

        // Start the coroutine to spawn objects
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            SpawnObjectAtSpawnerPosition();
            currentSpawnCount++;
            yield return new WaitForSeconds(spawnInterval); // Wait for specified seconds
        }

        Debug.Log("Max spawn count reached.");
    }

    private void SpawnObjectAtSpawnerPosition()
    {
        if (objectToSpawn != null)
        {
            // Spawn the object at the spawner's position
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Attempted to spawn a null object. Check objectToSpawn reference.");
        }
    }
}
