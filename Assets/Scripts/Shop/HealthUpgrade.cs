using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// The HealthUpgrade class upgrades the player's health
/// </summary>
public class HealthUpgrade : MonoBehaviour
{
    /// <summary>
    /// Text displaying the price of the health upgrade.
    /// </summary>
    public TextMeshProUGUI priceText;

    /// <summary>
    /// Text displaying the current max health value.
    /// </summary>
    public TextMeshProUGUI valueText;

    /// <summary>
    /// The price of the health upgrade.
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
                buyHp();
            }
        }
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        // Display current max health and price information
        priceText.text = "Health Price:" + price;
        valueText.text = "Current max health: " + Player.playerInstance.maxHealth;
    }

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    private void Start()
    {
        // Set the price from PriceStorage Class
        price = PriceStorage.instance.healthUpgradePrice;
    }

    /// <summary>
    /// Upgrades health and increases price 
    /// </summary>
    private void buyHp()
    {
        Player p = Player.playerInstance;
        Debug.Log(p.maxHealth);
        p.maxHealth += 5;
        p.currentMoney -= price;
        price += 50;
        PriceStorage.instance.healthUpgradePrice = price;
        p.currentHealth = p.maxHealth;
        p.setCurerntHealth(p.currentHealth);
        Debug.Log(p.maxHealth);
    }

    /// <summary>
    /// Called when another collider exits the trigger collider attached to this object.
    ///  Sets the isInShopAre to False
    /// </summary>
    /// <param name="collision">The Collider2D that exited the trigger.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.playerInstance.isInShopArea = false;
    }

    /// <summary>
    /// Called when another collider enters the trigger collider attached to this object.
    ///  Sets the isInShopAre to True
    /// </summary>
    /// <param name="collision">The Collider2D that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.playerInstance.isInShopArea = true;
    }
}
