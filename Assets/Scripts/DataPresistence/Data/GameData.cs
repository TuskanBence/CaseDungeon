using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the data associated with the game, including player statistics and inventory.
/// </summary>
[System.Serializable]
public class GameData
{
    /// <summary>
    /// The maximum health of the player.
    /// </summary>
    public int playerMaxHealth;

    /// <summary>
    /// The current health of the player.
    /// </summary>
    public int playerCurrentHealth;

    /// <summary>
    /// The amount of money the player has.
    /// </summary>
    public int playerMoney;

    /// <summary>
    /// The damage value of the player's attacks.
    /// </summary>
    public int playerDamage;

    /// <summary>
    /// The range of the player's attacks.
    /// </summary>
    public int playerRange;

    /// <summary>
    /// The price to upgrade the player's attack range.
    /// </summary>
    public int rangeUpgradePrice;

    /// <summary>
    /// The price to upgrade the player's damage.
    /// </summary>
    public int damageUpgradePrice;

    /// <summary>
    /// The price to upgrade the player's maximum health.
    /// </summary>
    public int healthUpgradePrice;

    /// <summary>
    /// The list of the player's cases.
    /// </summary>
    public List<CaseData> playerCases;

    /// <summary>
    /// The maximum carrying capacity of the player.
    /// </summary>
    public int playerMaxWeigth;

    /// <summary>
    /// The current weigth if cases carried by the player.
    /// </summary>
    public int playerCurrentWeigth;


    /// <summary>
    /// Initializes a new instance of the GameData class with default values.
    /// </summary>
    public GameData()
    {
        this.playerMoney = 0;
        this.playerRange = 3;
        this.playerDamage = 200;
        this.playerMaxHealth = 10;
        this.playerCurrentHealth = playerMaxHealth;
        this.rangeUpgradePrice = 500;
        this.damageUpgradePrice = 100;
        this.healthUpgradePrice = 100;
        this.playerCases = new List<CaseData>();
        this.playerMaxWeigth = 60;
        this.playerCurrentWeigth = 0;
    }
}
