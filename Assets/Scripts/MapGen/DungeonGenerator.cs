using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The DungeonGenerator class generates a dungeon and spawns rooms based on the DungeonCrawlers movement.
/// </summary>
public class DungeonGenerator : MonoBehaviour
{
    /// <summary>
    /// The data used for dungeon generation.
    /// </summary>
    public DungeonGenerationData dungeonGenerationData;
    public string[] potentialMaps;

    /// <summary>
    /// The list of room locations in the dungeon.
    /// </summary>
    private List<Vector2Int> dungeonRooms;

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    private void Start()
    {
        // Generate dungeon and spawn rooms
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }
    private string SelectRoom()
    {
        // Generate a random index within the bounds of the array
        int index = Random.Range(0, potentialMaps.Length);
        // Retrieve the random item from the array
        return potentialMaps[index];
    }
    /// <summary>
    /// Spawns rooms based on the provided room locations.
    /// </summary>
    /// <param name="rooms">The list of room locations to spawn.</param>
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        // Load the starting room
        RoomController.instance.LoadRoom(new RoomInfo("Start", 0, 0));

        // Load empty rooms based on the generated room locations
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom(new RoomInfo("Shrine", roomLocation.x, roomLocation.y));
        }
    }
}
