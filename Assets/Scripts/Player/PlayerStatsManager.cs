using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour,IDataPersistence
{
    public int playerMaxHealth=0;
    public int playerDamage=0;
    public int playerRange=0;
    public int playerMoney=0;
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
    public int getPlayerMoney()
    {
        return playerMoney;
    }
    public void addPlayerMaxHealth(int newMaxHealth)
    {
        playerMaxHealth += newMaxHealth;
    }
    public void addPlayerDamage(int newPlayerDamage)
    {
        playerDamage += newPlayerDamage;
    }
    public void addPlayerRange(int newPlayerRange)
    {
        playerRange = newPlayerRange;
    }
    public void addPlayerMoney(int newPlayerMoney)
    {
         playerMoney+=newPlayerMoney;
    }
    public void setPlayerMaxHealth(int newMaxHealth)
    {
        playerMaxHealth = newMaxHealth;
    }
    public void setPlayerDamage(int newPlayerDamage)
    {
        playerDamage = newPlayerDamage;
    }
    public void setPlayerRange(int newPlayerRange)
    {
        playerRange = newPlayerRange;
    }
    public void setPlayerMoney(int newPlayerMoney)
    {
        playerMoney = newPlayerMoney;
    }
    public void LoadData(GameData data)
    {
        if (data.fromShop)
        {
            return;
        }
        else
        {
            setPlayerDamage(data.playerDamage);
            setPlayerRange(data.playerRange);
            setPlayerMaxHealth(data.playerMaxHealth);
            setPlayerMoney(data.playerMoney);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerDamage = getPlayerDamage();
        data.playerRange = getPlayerRange();
        data.playerMaxHealth = getPlayerMaxHealth();
        data.playerMoney = getPlayerMoney();
    }
}
