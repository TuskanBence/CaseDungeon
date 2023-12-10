using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeUpgrade : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI valueText;
    public int price;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            if (Player.playerInstance.currentMoney >= price)
            {
                if (Player.playerInstance.range>=8)
                {
                    return;
                }
                else
                {
                    buyRange();
                }
                
            }
        }
    }
    private void Start()
    {
        price = PriceStorage.instance.rangeUpgradePrice;
    }
    private void Update()
    {
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
    private void buyRange()
    {
        Debug.Log("Range upgraded");
        Debug.Log(Player.playerInstance.range);
        Player.playerInstance.range+=1;
        Player.playerInstance.currentMoney -= price;
        price += 500;
        PriceStorage.instance.rangeUpgradePrice=price;
        Debug.Log(Player.playerInstance.range);
        Debug.Log(PriceStorage.instance.rangeUpgradePrice);
        Debug.Log("Range END");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.playerInstance.isInShopArea = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.playerInstance.isInShopArea = true;
    }

    
}
