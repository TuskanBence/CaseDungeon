using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// The EnemyGenerator class is responsible for generating and spawning enemies in the game.
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>
    /// Reference to the case prefab.
    /// </summary>
    public GameObject enemyPrefab;

    /// <summary>
    /// Point where the enemies will be spawned.
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// Represents a weighted item with a value and weight.
    /// </summary>
    [Serializable]
    public class WeightedItem
    {
        /// <summary>
        /// The value associated with the weighted item.
        /// </summary>
        public int value;

        /// <summary>
        /// The weight of the item, higher the weight higher the chance.
        /// </summary>
        public float weight;
    }

    /// <summary>
    /// List of weighted items.
    /// </summary>
    public List<WeightedItem> weightedItems;

    /// <summary>
    /// Gets a weighted random value.
    /// </summary>
    /// <returns>Weighted random value.</returns>
    int GetWeightedRandomValue()
    {
        float totalWeight = 0;

        // Calculate the total weight of all items
        foreach (WeightedItem item in weightedItems)
        {
            totalWeight += item.weight;
        }

        // Generate a random weight between 0 and the total weight
        float randomWeight = UnityEngine.Random.Range(0f, totalWeight);

        // Find the item corresponding to the generated weight
        foreach (WeightedItem item in weightedItems)
        {
            if (randomWeight <= item.weight)
            {
                return item.value;
            }

            randomWeight -= item.weight;
        }

        // This should not happen, but just in case
        Debug.LogError("Weighted random value calculation failed!");
        return -1;
    }
    /// <summary>
    /// Generates enemies spawns them at the specified location.
    /// </summary>
    public void GenerateEnemies()
    {
        int randomValue = GetWeightedRandomValue();

        // Spawn enemies based on the calculated random value
        for (int i = 0; i < randomValue; i++)
        {
            // Calculate a random position within the spawn point's bounds
            Vector2 rangeSize = spawnPoint.GetComponent<SpriteRenderer>().bounds.size;
            Vector2 randomPosition = new Vector2(
                spawnPoint.position.x + UnityEngine.Random.Range(-rangeSize.x / 2f, rangeSize.x / 2f),
                spawnPoint.position.y + UnityEngine.Random.Range(-rangeSize.y / 2f, rangeSize.y / 2f)
            );

            // Instantiate and position the enemy object
            GameObject enemyObject = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemyObject.transform.SetParent(spawnPoint.transform);
        }
    }
}
