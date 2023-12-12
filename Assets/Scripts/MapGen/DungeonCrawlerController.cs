using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller responsible for generating a dungeon layout using dungeon crawlers.
/// </summary>
public class DungeonCrawlerController : MonoBehaviour
{
    /// <summary>
    /// Enum representing the possible movement directions.
    /// </summary>
    public enum Directions
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3
    }

    /// <summary>
    /// List of positions visited during dungeon generation.
    /// </summary>
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    /// <summary>
    /// Dictionary mapping movement directions to Vector2Int.
    /// </summary>
    private static readonly Dictionary<Directions, Vector2Int> directionMovementMap = new Dictionary<Directions, Vector2Int>
    {
        { Directions.up, Vector2Int.up},
        { Directions.down, Vector2Int.down},
        { Directions.left, Vector2Int.left},
        { Directions.right, Vector2Int.right}
    };

    /// <summary>
    /// Generates a dungeon layout using dungeon crawlers.
    /// </summary>
    /// <param name="dungeonData">Dungeon generation data.</param>
    /// <returns>List of positions visited during dungeon generation by the dungeoncrawlers.</returns>
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        // Initialize dungeon crawlers at the starting position (zero).
        for (int i = 0; i < dungeonData.numberofCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }

        // Randomly determine the number of iterations for dungeon generation.
        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        // Perform dungeon generation iterations.
        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler d in dungeonCrawlers)
            {
                // Move each crawler and record the new position.
                Vector2Int newPos = d.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
    }
}
