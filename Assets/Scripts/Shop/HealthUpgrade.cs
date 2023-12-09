using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthUpgrade : MonoBehaviour
{

    public TextMeshProUGUI text;
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
        text.text = "Health Price:" + price;
    }
    private void buyHp()
    {
        Debug.Log(PlayerStatsManager.instance.getPlayerMaxHealth());
        PlayerStatsManager.instance.addPlayerMaxHealth(5);
        Player.playerInstance.currentMoney-=price;
        price += 50;
        Player.playerInstance.refreshStats();
        Debug.Log(PlayerStatsManager.instance.getPlayerMaxHealth());
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
