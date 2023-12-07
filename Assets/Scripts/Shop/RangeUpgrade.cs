using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeUpgrade : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int price = 100;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            if (Player.playerInstance.currentMoney >= price)
            {
                buyRange();
            }
        }
    }
    private void Update()
    {
        text.text = "Range Price:" + price;
    }
    private void buyRange()
    {
        Debug.Log("Range upgraded");
        Debug.Log(PlayerStatsManager.instance.getPlayerRange());
        PlayerStatsManager.instance.setPlayerRange(2);
        Player.playerInstance.currentMoney -= price;
        price += 50;
        Player.playerInstance.refreshStats();
        Debug.Log(PlayerStatsManager.instance.getPlayerRange());
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
