using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour
{
    public TextMeshProUGUI text;
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
    private void Update()
    {
        text.text = "Damage Price:" + price;
    }
    private void buyDamage()
    {
       
        PlayerStatsManager.instance.addPlayerDamage(5);
        Player.playerInstance.currentMoney -= price;
        price += 50;
        Player.playerInstance.refreshStats();
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
