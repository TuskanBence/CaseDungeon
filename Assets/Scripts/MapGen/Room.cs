using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    bool playin;
    bool cleared;
    public int Width;
    public int Height;
    public int Y;
    public int X;

    public GameObject spawnPoint;
    List<EnemyAI> enemies = new List<EnemyAI>();
    private bool updatedDoors = false;
    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("Worng scene");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doortype)
            {
                case Door.DoorType.right:
                    rightDoor = d; break;
                case Door.DoorType.left:
                    leftDoor = d; break;
                case Door.DoorType.top:
                    topDoor = d; break;
                case Door.DoorType.bottom:
                    bottomDoor = d; break;
            }
        }
        RoomController.instance.RegisterRroom(this);
        if (enemies == null)
        {
            enemies = new List<EnemyAI>();
        }
    }
    private void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
        if (enemies.Count==0 && playin && !cleared)
        {
            doorControll(false);
            cleared = true;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }
    //Returns the center of the room
    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }
    //Checks if a player collided with a collider2d
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            playin = true; if (!cleared)
            {
                doorControll(true);
            }
            foreach (Transform item in spawnPoint.transform)
            {
                if (item.CompareTag("Enemy"))
                {
                    EnemyAI e = item.gameObject.GetComponent<EnemyAI>();
                    e.setRoom(this);
                    enemies.Add(e);
                }

            }
        }

    }

    private void doorControll(bool state)
    {
        foreach ( Door d in doors)
        {
            d.gameObject.SetActive(state);
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doortype)
            {
                case Door.DoorType.right:
                    if (GetRight() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.nowhere = true;
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.nowhere = true;
                    }
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.nowhere = true;
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.nowhere = true;
                    }
                    break;
            }
        }
        doors.RemoveAll(door => door.nowhere);
        doorControll(false);
    }

    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    internal void removeEnemy(EnemyAI enemy)
    {
        enemies.Remove(enemy);
    }
}
