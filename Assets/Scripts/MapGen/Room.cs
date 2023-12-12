using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The Room class represents a room in the game and handles room-specific logic.
/// </summary>
public class Room : MonoBehaviour
{
    /// <summary>
    /// Indicates whether the player is currently inside the room.
    /// </summary>
    private bool playin;

    /// <summary>
    /// Indicates whether the room has been cleared of enemies.
    /// </summary>
    public bool cleared;

    /// <summary>
    /// The width of the room.
    /// </summary>
    public int Width;

    /// <summary>
    /// The height of the room.
    /// </summary>
    public int Height;

    /// <summary>
    /// The X coordinate of the room.
    /// </summary>
    public int X;

    /// <summary>
    /// The Y coordinate of the room.
    /// </summary>
    public int Y;

    /// <summary>
    /// The spawn point in the room.
    /// </summary>
    public GameObject spawnPoint;

    /// <summary>
    /// List of enemies in the room.
    /// </summary>
    public List<EnemyAI> enemies = new List<EnemyAI>();

    /// <summary>
    /// List of cases in the room.
    /// </summary>
    public List<Case> cases = new List<Case>();

    /// <summary>
    /// Indicates whether doors have been updated.
    /// </summary>
    private bool updatedDoors = false;

    /// <summary>
    /// The left door of the room.
    /// </summary>
    public Door leftDoor;

    /// <summary>
    /// The right door of the room.
    /// </summary>
    public Door rightDoor;

    /// <summary>
    /// The top door of the room.
    /// </summary>
    public Door topDoor;

    /// <summary>
    /// The bottom door of the room.
    /// </summary>
    public Door bottomDoor;

    /// <summary>
    /// List of all doors in the room.
    /// </summary>
    public List<Door> doors = new List<Door>();

     /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("Wrong scene");
            return;
        }

        // Get all doors in the room
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

        // Register the room with the RoomController
        RoomController.instance.RegisterRroom(this);

        // Initialize lists if null
        if (enemies == null)
        {
            enemies = new List<EnemyAI>();
        }
        if (cases == null)
        {
            cases = new List<Case>();
        }

        // Spawn cases if the spawn point has a CaseGenerator component
        CaseGenerator g = spawnPoint.GetComponent<CaseGenerator>();
        if (g != null)
        {
            g.SpawnCases();
        }

        // Generate enemies if the room has not been cleared
        if (!cleared)
        {
            EnemyGenerator e = spawnPoint.GetComponent<EnemyGenerator>();
            if (e != null)
            {
                e.GenerateEnemies();
            }
        }
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        // Update doors for the "End" room
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }

        // Check if all enemies are defeated and the room is not cleared
        if (enemies.Count == 0 && playin && !cleared)
        {
            doorControll(false);
            cleared = true;
        }
    }
    /// <summary>
    /// Returns the center of the room.
    /// </summary>
    /// <returns>The center of the room as a Vector3.</returns>
    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }

    /// <summary>
    /// Called when a collider enters the trigger.
    /// </summary>
    /// <param name="other">The Collider2D that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.tag.CompareTo("Player") == 0)
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            playin = true;

            // If the room is not cleared, enable doors
            if (!cleared)
            {
                doorControll(true);
            }

            // Add enemies to the room's enemy list
            foreach (Transform item in spawnPoint.transform)
            {
                if (item.CompareTag("Enemy"))
                {
                    EnemyAI e = item.gameObject.GetComponent<EnemyAI>();
                    e.setRoom(this);
                    e.setPlayer(other.gameObject.transform);
                    enemies.Add(e);
                }
            }
        }
    }

    /// <summary>
    /// Controls the visibility of doors in the room.
    /// </summary>
    /// <param name="state">The visibility state of the doors.</param>
    private void doorControll(bool state)
    {
        foreach (Door d in doors)
        {
            d.gameObject.SetActive(state);
        }
    }

    /// <summary>
    /// Disables door that lead to nowhere.
    /// </summary>
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

        // Remove doors marked as nowhere
        doors.RemoveAll(door => door.nowhere);
        doorControll(false);
    }

    /// <summary>
    /// Returns the room to the left, if it exists.
    /// </summary>
    /// <returns>The room to the left or null if it doesn't exist.</returns>
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    /// <summary>
    /// Returns the room to the right, if it exists.
    /// </summary>
    /// <returns>The room to the right or null if it doesn't exist.</returns>
    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    /// <summary>
    /// Returns the room above, if it exists.
    /// </summary>
    /// <returns>The room above or null if it doesn't exist.</returns>
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    /// <summary>
    /// Returns the room below, if it exists.
    /// </summary>
    /// <returns>The room below or null if it doesn't exist.</returns>
    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    /// <summary>
    /// Removes an enemy from the room's enemy list.
    /// </summary>
    /// <param name="enemy">The enemy to remove.</param>
    internal void removeEnemy(EnemyAI enemy)
    {
        enemies.Remove(enemy);
    }
}
