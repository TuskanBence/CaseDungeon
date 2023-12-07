using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public int playerMaxHealth = 10;
    public int playerDamage = 2;
    public int playerRange = 6;
    public static PlayerStatsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public int getPlayerMaxHealth()
    {
        return playerMaxHealth;
    }
    public int getPlayerDamage()
    {
        return playerDamage;
    }
    public int getPlayerRange()
    {
        return playerRange;
    }
    public void setPlayerMaxHealth(int newMaxHealth)
    {
        playerMaxHealth += newMaxHealth;
    }
    public void setPlayerDamage(int newPlayerDamage)
    {
        playerDamage += newPlayerDamage;
    }
    public void setPlayerRange(int newPlayerRange)
    {
        playerRange += newPlayerRange;
    }
}
