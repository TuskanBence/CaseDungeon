using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DungeonCrawlerController;

/// <summary>
/// Represents a dungeon crawler used for dungeon layout generation.
/// </summary>
public class DungeonCrawler
{
    /// <summary>
    /// Gets or sets the position of the dungeon crawler.
    /// </summary>
    public Vector2Int Position { get; set; }

    /// <summary>
    /// Constructor for <see cref="DungeonCrawler"/> class.
    /// </summary>
    /// <param name="startPos">Starting position of the dungeon crawler.</param>
    public DungeonCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }

    /// <summary>
    /// Moves the dungeon crawler in a random direction based on the provided direction-movement map.
    /// </summary>
    /// <param name="directionMovementMap">Dictionary mapping movement directions to Vector2Int.</param>
    /// <returns>The new position of the dungeon crawler after movement.</returns>
    public Vector2Int Move(Dictionary<Directions, Vector2Int> directionMovementMap)
    {
        Directions toMove = (Directions)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;

    }
}
