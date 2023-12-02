using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonCrawlerController : MonoBehaviour
{
    public enum Directions
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3
    }
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Directions, Vector2Int> directionMovementMap = new Dictionary<Directions, Vector2Int>
    {
        { Directions.up,Vector2Int.up},
        { Directions.down,Vector2Int.down},
        { Directions.left,Vector2Int.left},
        { Directions.right,Vector2Int.right}
    };
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();
        for (int i = 0; i < dungeonData.numberofCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler d in dungeonCrawlers)
            {
                Vector2Int newPos = d.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }
        return positionsVisited;
    }
}
