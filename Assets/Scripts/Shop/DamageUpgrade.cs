using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The DamageUpgrade class upgrades the player's damage.
/// </summary>
public class DamageUpgrade : MonoBehaviour
{
    /// <summary>
    /// Text displaying the price of the damage upgrade.
    /// </summary>
    public TextMeshProUGUI priceText;

    /// <summary>
    /// Text displaying the current damage value.
    /// </summary>
    public TextMeshProUGUI valueText;

    /// <summary>
    /// The price of the damage upgrade.
    /// </summary>
    public int price = 100;

    /// <summary>
    /// Called when another collider stays inside the trigger collider attached to this object.
    /// </summary>
    /// <param name="collision">The Collider2D that stayed in the trigger.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player is in the trigger area and pressed the 'F' key
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            // Check if the player has enough money
            if (Player.playerInstance.currentMoney >= price)
            {
                buyDamage();
            }
        }
    }

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    private void Start()
    {
        // Sets the price from PriceStorage Class
        price = PriceStorage.instance.damageUpgradePrice;
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        // Display current damage and price information
        valueText.text = "Current damage: " + Player.playerInstance.getDamage();
        priceText.text = "Damage Price:" + price;
    }

    /// <summary>
    /// Upgrades damage and increases price 
    /// </summary>
    private void buyDamage()
    {
        Debug.Log("Damage upgrade");
        Debug.Log(Player.playerInstance.damage);
        Player.playerInstance.damage += 1;
        Player.playerInstance.currentMoney -= price;
        price += 100;
        PriceStorage.instance.damageUpgradePrice = price;
        Debug.Log(Player.playerInstance.damage);
        Debug.Log(PriceStorage.instance.damageUpgradePrice);
        Debug.Log("Damage END");
    }

    /// <summary>
    /// Called when another collider exits the trigger collider attached to this object.
    /// Sets the isInShopAre to False
    /// </summary>
    /// <param name="collision">The Collider2D that exited the trigger.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.playerInstance.isInShopArea = false;
    }

    /// <summary>
    /// Called when another collider enters the trigger collider attached to this object.
    /// Sets the isInShopAre to True
    /// </summary>
    /// <param name="collision">The Collider2D that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.playerInstance.isInShopArea = true;
    }
}
