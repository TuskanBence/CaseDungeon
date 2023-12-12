using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// The CaseGenerator class is responsible for generating and spawning cases in the game.
/// </summary>
public class CaseGenerator : MonoBehaviour
{
    /// <summary>
    /// Reference to the case prefab.
    /// </summary>
    public GameObject casePrefab;

    /// <summary>
    /// Point where the case could be spawned.
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// Represents a weighted item with a value and weight.
    /// </summary>
    [Serializable]
    public class WeightedItem
    {
        /// <summary>
        /// The value of the weighted item.
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
        Debug.LogError("Weighted random value calculation failed!");
        return -1;
    }

    /// <summary>
    /// Generates a case with random attributes.
    /// </summary>
    /// <returns>Randomly generated case data.</returns>
    private CaseData GenerateRandomCase()
    {
        CaseData randomCase = new CaseData();
        Array sizes = Enum.GetValues(typeof(Case.Size));
        Array rarities = Enum.GetValues(typeof(Case.Rarity));

        randomCase.caseWeight = UnityEngine.Random.Range(1, 10);
        randomCase.caseSize = (Case.Size)sizes.GetValue(UnityEngine.Random.Range(0, sizes.Length));
        randomCase.caseRarity = (Case.Rarity)rarities.GetValue(UnityEngine.Random.Range(0, rarities.Length));
        int value = UnityEngine.Random.Range(1, 10);
        randomCase.caseValue = value * (int)randomCase.caseSize * (int)randomCase.caseRarity * randomCase.caseWeight;

        return randomCase;
    }

    /// <summary>
    /// Spawns a number of cases based on a weighted random value.
    /// </summary>
    public void SpawnCases()
    {
        int randomValue = GetWeightedRandomValue();
        for (int i = 0; i < randomValue; i++)
        {
            CaseData randomCase = GenerateRandomCase();
            spawnRandomCase(randomCase);
        }
    }

    /// <summary>
    /// Spawns a case at a random position within the spawn point's range.
    /// </summary>
    /// <param name="newCase">Case data for the spawned case.</param>
    private void spawnRandomCase(CaseData newCase)
    {
        Vector2 rangeSize = spawnPoint.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 randomPosition = new Vector2(
           spawnPoint.position.x + UnityEngine.Random.Range(-rangeSize.x / 2f, rangeSize.x / 2f),
           spawnPoint.position.y + UnityEngine.Random.Range(-rangeSize.y / 2f, rangeSize.y / 2f)
       );

        // Instantiate the case prefab at the spawn point
        Room r = spawnPoint.GetComponentInParent<Room>();
        GameObject caseObject = Instantiate(casePrefab, randomPosition, Quaternion.identity);
        caseObject.transform.SetParent(spawnPoint.transform);

        // Access the CaseComponent script on the instantiated object
        Case caseComponent = caseObject.GetComponent<Case>();
        caseComponent.Initialize(newCase);
        r.cases.Add(caseComponent);
    }
}
