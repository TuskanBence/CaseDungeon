using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for storing and managing prices for game upgrades.
/// </summary>
public class PriceStorage : MonoBehaviour, IDataPersistence
{
    /// <summary>
    /// Price for the range upgrade.
    /// </summary>
    public int rangeUpgradePrice;

    /// <summary>
    /// Price for the damage upgrade.
    /// </summary>
    public int damageUpgradePrice;

    /// <summary>
    /// Price for the health upgrade.
    /// </summary>
    public int healthUpgradePrice;

    /// <summary>
    /// Static instance of the PriceStorage class for singleton pattern.
    /// </summary>
    public static PriceStorage instance;

    /// <summary>
    /// Loads data from a GameData object into the PriceStorage.
    /// </summary>
    /// <param name="data">The GameData object containing saved data.</param>
    public void LoadData(GameData data)
    {
        rangeUpgradePrice = data.rangeUpgradePrice;
        damageUpgradePrice = data.damageUpgradePrice;
        healthUpgradePrice = data.healthUpgradePrice;
    }

    /// <summary>
    /// Saves data from the PriceStorage into a GameData object.
    /// </summary>
    /// <param name="data">The GameData object to be updated with current data.</param>
    public void SaveData(ref GameData data)
    {
        data.rangeUpgradePrice = rangeUpgradePrice;
        data.damageUpgradePrice = damageUpgradePrice;
        data.healthUpgradePrice = healthUpgradePrice;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // If an instance already exists, destroy the duplicate instance
            Destroy(gameObject);
        }
    }
}
