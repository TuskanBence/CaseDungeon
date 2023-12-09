using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerMoney;
    public int playerDamage;
    public int playerMaxHealth;
    public int playerRange;
    public bool fromShop;
    public Vector3 playerPosition;
    public List<CaseData> playerCases;
    public List<savedRoom> rooms;

    [System.Serializable]
    public struct savedRoom
    {
        public RoomInfo room;
        public List<EnemyAI> enemies;

    }

    public GameData()
    {
        this.playerMoney = 0;
        this.playerRange = 6;
        this.playerDamage =20;
        this.playerMaxHealth = 10;
        playerPosition = Vector3.zero;
        playerCases = new List<CaseData>();
        rooms = new List<savedRoom>();
}
}
