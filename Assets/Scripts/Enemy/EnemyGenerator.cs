using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPefab; // Reference to the case prefab in the Unity Editor
    public Transform spawnPoint; // Point where the case will be spawned
    [Serializable]
    public class WeightedItem
    {
        public int value;
        public float weight;
    }

    public List<WeightedItem> weightedItems;

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
   

    private void Start()
    {
        
    }

    public void GenerateEnemies()
    {
        int randomValue = GetWeightedRandomValue();
        for (int i = 0; i < randomValue; i++)
        {
            Vector2 rangeSize = spawnPoint.GetComponent<SpriteRenderer>().bounds.size;
            Vector2 randomPosition = new Vector2(
               spawnPoint.position.x + UnityEngine.Random.Range(-rangeSize.x / 2f, rangeSize.x / 2f),
               spawnPoint.position.y + UnityEngine.Random.Range(-rangeSize.y / 2f, rangeSize.y / 2f)
           );
            GameObject caseObject = Instantiate(enemyPefab, randomPosition, Quaternion.identity);
            caseObject.transform.SetParent(spawnPoint.transform);
        }
    }
}
