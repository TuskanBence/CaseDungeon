using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The RangeUpgrade class upgrades the player's range
/// </summary>
public class RangeUpgrade : MonoBehaviour
{
    /// <summary>
    /// Text displaying the price of the range upgrade.
    /// </summary>
    public TextMeshProUGUI priceText;

    /// <summary>
    /// Text displaying the current range value.
    /// </summary>
    public TextMeshProUGUI valueText;

    /// <summary>
    /// The price of the range upgrade.
    /// </summary>
    public int price;

    /// <summary>
    /// Called when another collider stays inside the trigger collider attached to this object.
    /// </summary>
    /// <param name="collision">The Collider2D that stayed in the trigger.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player is in the trigger area and if "F" key is pressed
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            // Check if the player has enough money and has not reached the maximum range
            if (Player.playerInstance.currentMoney >= price && Player.playerInstance.range < 8)
            {
                buyRange();
            }
        }
    }

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    private void Start()
    {
        // Sets the price from PriceStorage Class
        price = PriceStorage.instance.rangeUpgradePrice;
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        // Display current range and price information
        if (Player.playerInstance.range >= 8)
        {
            valueText.text = "Current range: " + Player.playerInstance.getRange();
            priceText.text = "You have reached max range";
        }
        else
        {
            valueText.text = "Current range: " + Player.playerInstance.getRange();
            priceText.text = "Range Price:" + price;
        }
    }

    /// <summary>
    /// Upgrades range and increases price 
    /// </summary>
    private void buyRange()
    {
        Debug.Log("Range upgraded");
        Debug.Log(Player.playerInstance.range);
        Player.playerInstance.range += 1;
        Player.playerInstance.currentMoney -= price;
        price += 500;
        PriceStorage.instance.rangeUpgradePrice = price;
        Debug.Log(Player.playerInstance.range);
        Debug.Log(PriceStorage.instance.rangeUpgradePrice);
        Debug.Log("Range END");
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
