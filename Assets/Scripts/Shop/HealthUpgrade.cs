using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthUpgrade : MonoBehaviour
{

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI valueText;
    public int price=100;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            if(Player.playerInstance.currentMoney>=price){
                buyHp();
            }
            
        }
    }
    private void Update()
    {
        priceText.text = "Health Price:" + price;
        valueText.text = "Current max health: " + Player.playerInstance.maxHealth;
    }
    private void Start()
    {
      
        price = PriceStorage.instance.healthUpgradePrice;
    }
    private void buyHp()
    {
        Player p = Player.playerInstance;
        Debug.Log(p.maxHealth);
        p.maxHealth+=5;
       p.currentMoney-=price;
        price += 50;
        PriceStorage.instance.healthUpgradePrice = price;
        p.currentHealth = p.maxHealth;
        p.setCurerntHealth(p.currentHealth);
        Debug.Log(p.maxHealth);
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
