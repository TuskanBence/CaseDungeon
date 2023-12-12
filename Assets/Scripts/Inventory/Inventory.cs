using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's inventory.
/// </summary>
public class Inventory : MonoBehaviour, IDataPersistence
{
    /// <summary>
    /// The prefab used to instantiate cases in the inventory.
    /// </summary>
    public GameObject casePrefab;

    /// <summary>
    /// The list of cases currently in the inventory.
    /// </summary>
    public List<Case> cases = new List<Case>();
    
    /// <summary>
    /// Static reference to the Inventory instance.
    /// </summary>
    public static Inventory Instance;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        // Ensure there is only one instance of Inventory.
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Adds a case to the inventory.
    /// </summary>
    /// <param name="c">The case to add.</param>
    public void Add(Case c)
    {
        cases.Add(c);
    }

    /// <summary>
    /// Removes a case from the inventory.
    /// </summary>
    /// <param name="c">The case to remove.</param>
    public void Remove(Case c)
    {
        cases.Remove(c);
    }

    /// <summary>
    /// Creates a new case using the provided case data.
    /// </summary>
    /// <param name="current">The case data to initialize the case with.</param>
    /// <returns>The created case component.</returns>
    public Case CreateCase(CaseData current)
    {
        GameObject caseObject = Instantiate(casePrefab, new Vector2(0, 0), Quaternion.identity);
        Case caseComponent = caseObject.GetComponent<Case>();
        caseComponent.Initialize(current);
        return caseComponent;
    }

    /// <summary>
    /// Loads inventory data from the provided game data.
    /// </summary>
    /// <param name="data">The game data to load from.</param>
    public void LoadData(GameData data)
    {
        foreach (CaseData currentCase in data.playerCases)
        {
            cases.Add(CreateCase(currentCase));
        }
    }

    /// <summary>
    /// Saves inventory data to the provided game data.
    /// </summary>
    /// <param name="data">The game data to save to.</param>
    public void SaveData(ref GameData data)
    {
        data.playerCases.Clear();
        foreach (Case currentCase in cases)
        {
            CaseData c = new CaseData
            {
                caseRarity = currentCase.caseRarity,
                caseValue = currentCase.caseValue,
                caseWeight = currentCase.caseWeight,
                caseSize = currentCase.caseSize
            };

            data.playerCases.Add(c);
        }
    }
}
