using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using static GameData;
using Unity.VisualScripting;

/// <summary>
/// Represents the information about a room.
/// </summary>
[System.Serializable]
public class RoomInfo
{
    /// <summary>
    /// Name of the room.
    /// </summary>
    public string name;

    /// <summary>
    /// X coordinate of the room.
    /// </summary>
    public int X;

    /// <summary>
    /// Y coordinate of the room.
    /// </summary>
    public int Y;

    /// <summary>
    /// Shows true if room if cleared false if not.
    /// </summary>
    public bool cleared;

    /// <summary>
    /// List of cases in the room.
    /// </summary>
    public List<CaseData> cases;

    /// <summary>
    /// Constructor for <see cref="RoomInfo"/> class.
    /// </summary>
    /// <param name="name">The name of the room.</param>
    /// <param name="x">The X coordinate of the room.</param>
    /// <param name="y">The Y coordinate of the room.</param>
    public RoomInfo(string name, int x, int y)
    {
        this.name = name;
        X = x;
        Y = y;
        cases = new List<CaseData>();
        cleared = false;
    }
}

/// <summary>
/// Controls the loading and management of rooms in the game.
/// </summary>
public class RoomController : MonoBehaviour
{
    /// <summary>
    /// Static reference to the RoomController instance.
    /// </summary>
    public static RoomController instance;

    /// <summary>
    /// The current room the player is in.
    /// </summary>
    Room currentRoom;

    /// <summary>
    /// The name of the current world.
    /// </summary>
    string currentWorldName = "Basement";

    /// <summary>
    /// Currently loaded rooms info
    /// </summary>
    RoomInfo currentLoadRoomData;

    /// <summary>
    /// The queue of rooms to be loaded.
    /// </summary>
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    /// <summary>
    /// List of already loaded rooms
    /// </summary>
    public List<Room> loadedRooms = new List<Room>();

    /// <summary>
    /// List of potencial rooms
    /// </summary>
    public List<RoomInfo> potencialrooms = new List<RoomInfo>();

    /// <summary>
    /// Shows if a room is currently being loaded.
    /// </summary>
    bool isLoadingRoom = false;

    /// <summary>
    /// Shows if a boss room has been spawned.
    /// </summary>
    bool spawnedBossRoom = false;

    /// <summary>
    /// Shows if the rooms have been updated.
    /// </summary>
    bool updatedRooms;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Gets the current room.
    /// </summary>
    /// <returns>The current room.</returns>
    public Room getCurrentRoom()
    {
        return currentRoom;
    }

    /// <summary>
    /// Checks if a specific room exists based on coordinates.
    /// </summary>
    /// <param name="x">The X coordinate of the room.</param>
    /// <param name="y">The Y coordinate of the room.</param>
    /// <returns> true if the room exists; otherwise, false.</returns>
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    /// <summary>
    /// Finds a room based on coordinates.
    /// </summary>
    /// <param name="x">The X coordinate of the room.</param>
    /// <param name="y">The Y coordinate of the room.</param>
    /// <returns>The room if found; otherwise, null.</returns>
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    /// <summary>
    /// Called when the player enters a room.
    /// </summary>
    /// <param name="room">The entered room.</param>
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }

    /// <summary>
    /// Loads a room into the game.
    /// </summary>
    /// <param name="newroom">The room information to load.</param>
    public void LoadRoom(RoomInfo newroom)
    {
        if (DoesRoomExist(newroom.X, newroom.Y) || loadRoomQueue.Any(room => room.X == newroom.X && room.Y == newroom.Y))
        {
            return;
        }

        loadRoomQueue.Enqueue(newroom);
    }
    /// <summary>
    /// Loads a room routine.
    /// </summary>
    /// <param name="info">The room information to load.</param>
    /// <returns>An enumerator for the coroutine.</returns>
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Registers a room in the game.
    /// </summary>
    /// <param name="room">The room to register.</param>
    public void RegisterRroom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentLoadRoomData.name;
            room.transform.parent = transform;
            isLoadingRoom = false;
            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currentRoom = room;
            }
            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    /// <summary>
    /// Updates the room queue and spawns boss rooms if needed.
    /// </summary>
    void UpdateRoomQueue()
    {
        if (isLoadingRoom) { return; }
        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }

            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    /// <summary>
    /// Spawns a boss room.
    /// </summary>
    /// <returns>An enumerator for the coroutine.</returns>
    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Vector2Int temproom = new Vector2Int(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == temproom.x && r.Y == temproom.y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom(new RoomInfo("End", temproom.x, temproom.y));
        }
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    void Update()
    {
        UpdateRoomQueue();
    }
}
