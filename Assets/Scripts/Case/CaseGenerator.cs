using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class CaseGenerator : MonoBehaviour
{
    public GameObject casePrefab; // Reference to the case prefab in the Unity Editor
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
    private CaseData GenerateRandomCase()
    {
        CaseData randomCase = new CaseData();
        Array sizes = Enum.GetValues(typeof(Case.Size));
        Array rarities = Enum.GetValues(typeof(Case.Rarity));

        randomCase.caseValue = UnityEngine.Random.Range(10, 101);
        randomCase.caseWeight = UnityEngine.Random.Range(1f, 10f);
        randomCase.caseSize = (Case.Size)sizes.GetValue(UnityEngine.Random.Range(0,sizes.Length));
        randomCase.caseRarity = (Case.Rarity)rarities.GetValue(UnityEngine.Random.Range(0, rarities.Length));

      

        return randomCase;
    }

    private void Start()
    {
        int randomValue = GetWeightedRandomValue();
        for (int i = 0; i < randomValue; i++)
        {
            CaseData randomCase = GenerateRandomCase();
            SpawnCase(randomCase);
        }
    }

    private void SpawnCase(CaseData newCase)
    {
        
        Vector2 rangeSize = spawnPoint.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 randomPosition = new Vector2(
           spawnPoint.position.x + UnityEngine.Random.Range(-rangeSize.x / 2f, rangeSize.x / 2f),
           spawnPoint.position.y + UnityEngine.Random.Range(-rangeSize.y / 2f, rangeSize.y / 2f)
       );
        // Instantiate the case prefab at the spawn point
        GameObject caseObject = Instantiate(casePrefab, randomPosition, Quaternion.identity);
        caseObject.transform.SetParent(spawnPoint.transform);
        // Access the CaseComponent script on the instantiated object
        Case caseComponent = caseObject.GetComponent<Case>();
        caseComponent.Initialize(newCase);
    }
}
