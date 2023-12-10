using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceStorage : MonoBehaviour, IDataPersistence
{
    public int rangeUpgradePrice;
    public int damageUpgradePrice;
    public int healthUpgradePrice;
    public static PriceStorage instance;

    public void LoadData(GameData data)
    {
        rangeUpgradePrice = data.rangeUpgradePrice;
        damageUpgradePrice = data.damageUpgradePrice;
        healthUpgradePrice = data.healthUpgradePrice;
    }

    public void SaveData(ref GameData data)
    {
         data.rangeUpgradePrice= rangeUpgradePrice;
         data.damageUpgradePrice= damageUpgradePrice;
         data.healthUpgradePrice= healthUpgradePrice;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
