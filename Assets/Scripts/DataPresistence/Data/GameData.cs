using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money;
    public Vector3 playerPosition;
    public List<Room> rooms;

    public GameData()
    {
        this.money = 10;
        playerPosition = Vector3.zero;
        rooms = null;
    }
}
