using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using static GameData;
using Unity.VisualScripting;

[System.Serializable]
public class RoomInfo
{
    public string name;
    public int X; // Location from start room
    public int Y;
    public bool fromSave;
    public bool cleared;
    public List<CaseData> cases;
    public RoomInfo(string name, int x, int y)
    {
        this.name = name;
        X = x;
        Y = y;
        cases=new List<CaseData>();
        cleared = false;
    }
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    Room currentRoom;

    string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    public List<RoomInfo> potencialrooms = new List<RoomInfo>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms;

    private void Awake()
    {
        instance = this;

    }
    public Room getCurrentRoom()
    {
        return currentRoom;
    }
    //Checks if a specific room exisits based on cordinates
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
    //When player enters another room set it to current room and set the camera
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }
    //Creates a room and places it inside a queue
    public void LoadRoom(RoomInfo newroom)
    { 
        if (DoesRoomExist(newroom.X, newroom.Y)|| loadRoomQueue.Any(room => room.X == newroom.X && room.Y == newroom.Y))
        {
            return;
        }
        
        loadRoomQueue.Enqueue(newroom);
    }
    public void addRoom(RoomInfo r)
    {
        potencialrooms.Add(r);
    }
    //Loads the rooms into the main scene
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }
    //Creates all the rooms based on the previously provided info and places them into a list
    public void RegisterRroom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            // room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
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
    

    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
       
    }
    //Checks if there are rooms to load if there are it starts to load them
    void UpdateRoomQueue()
    {
       
        if (isLoadingRoom) { return; }
        if (loadRoomQueue.Count == 0) {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if(spawnedBossRoom && !updatedRooms)
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
    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count==0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room temproom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == temproom.X && r.Y == temproom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom(new RoomInfo("End", temproom.X, temproom.Y));

        }
    }  
}
