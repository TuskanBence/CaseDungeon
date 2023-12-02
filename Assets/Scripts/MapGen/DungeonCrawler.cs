using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static DungeonCrawlerController;

public class DungeonCrawler
{
    public Vector2Int Position { get; set; }
    public DungeonCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }
    public Vector2Int Move(Dictionary<Directions, Vector2Int> directionMovementMap)
    {
        Directions toMove = (Directions)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;

    }
}
