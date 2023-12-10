using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerMaxHealth;
    public int playerCurrentHealth;
    public int playerMoney;
    public int playerDamage;
    public int playerRange;
    public int rangeUpgradePrice;
    public int damageUpgradePrice;
    public int healthUpgradePrice;
    public List<CaseData> playerCases;

    public GameData()
    {
        this.playerMoney = 0;
        this.playerRange = 3;
        this.playerDamage = 2;
        this.playerMaxHealth = 10;
        this.playerCurrentHealth = playerMaxHealth;
        this.rangeUpgradePrice = 500;
        this.damageUpgradePrice = 100;
        this.healthUpgradePrice = 100;
        this.playerCases = new List<CaseData>();
    }
}
