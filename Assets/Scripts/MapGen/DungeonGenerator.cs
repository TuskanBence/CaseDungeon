using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
  public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;
    private void Start()
    {
        RoomController.instance.potencialrooms.Clear(); 
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
       
    }
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
      
            RoomController.instance.addRoom(new RoomInfo("Start", 0, 0));
            foreach (Vector2Int roomLocation in rooms)
            {
                RoomController.instance.addRoom(new RoomInfo("Empty", roomLocation.x, roomLocation.y));
            }
        } 
}
