using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI valueText;
    public int price=100;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {

            if (Player.playerInstance.currentMoney >= price)
            {
                buyDamage();
            }
           
        }
    }
    private void Start()
    {
        price = PriceStorage.instance.damageUpgradePrice;
    }
    private void Update()
    {
        valueText.text = "Current damage: " + Player.playerInstance.getDamage();
        priceText.text = "Damage Price:" + price;
    }
    private void buyDamage()
    {
        Debug.Log("DAMage upgrade");
        Debug.Log(Player.playerInstance.damage);
        Player.playerInstance.damage+=1;
        Player.playerInstance.currentMoney -= price;
        price += 100;
        PriceStorage.instance.damageUpgradePrice = price;
        Debug.Log(Player.playerInstance.damage);
        Debug.Log(PriceStorage.instance.damageUpgradePrice);
        Debug.Log("DAMAGE END");
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
