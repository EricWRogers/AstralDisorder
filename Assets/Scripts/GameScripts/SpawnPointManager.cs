using System.Collections;
using System.Collections.Generic;
using OmnicatLabs.Extensions;
using OmnicatLabs.Timers;
using OmnicatLabs.Utils;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public GameObject staminaItemPrefab;
    public int numberOfItemsToSpawn;
    
    [Header("Spawn Points Variables")]
    private int totalSpawnPoints;
    private List<GameObject> spawnedItems = new List<GameObject>();

    public List<Transform> spawnPoints = new List<Transform>();
    private List<Transform> managedSpawnPoints = new List<Transform>();

    void Start()
    {
        SaveManager.Instance.onReset.AddListener(ResetPickups);

        managedSpawnPoints.AddRange(spawnPoints);

        totalSpawnPoints = spawnPoints.Count;

        SpawnStaminaItems();
    }

    void SpawnStaminaItems()
    {
        // Checks to makes sure that the number of items spawned does not exceed the number of available spawn points
        if (numberOfItemsToSpawn > totalSpawnPoints)
        {
            Debug.LogError("Error: The number of items to spawn cannot exceed the total number of spawn points avaliable.");
            
            numberOfItemsToSpawn = totalSpawnPoints;
        }

        int itemsSpawned = 0;

        while (itemsSpawned < numberOfItemsToSpawn)
        {
            int randomIndex = Random.Range(0, managedSpawnPoints.Count);
            Transform spawnPoint = managedSpawnPoints[randomIndex];

            GameObject staminaItem = Instantiate(staminaItemPrefab, spawnPoint.position, Quaternion.Euler(Random.Range(0f, 90f), Random.Range(0f, 90f), Random.Range(0f, 90f)));

            managedSpawnPoints.RemoveAt(randomIndex);
            spawnedItems.Add(staminaItem);
            itemsSpawned++;
        }
    }

    private void ResetPickups()
    {
        spawnedItems.ForEach(item => Destroy(item));
        spawnedItems.Clear();
        managedSpawnPoints.Clear();
        managedSpawnPoints.AddRange(spawnPoints);
        SpawnStaminaItems();
    }
}
