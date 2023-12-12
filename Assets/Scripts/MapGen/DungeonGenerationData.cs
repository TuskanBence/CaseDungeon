using UnityEngine;

/// <summary>
/// ScriptableObject for storing dungeon generation data.
/// </summary>
[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    /// <summary>
    /// Number of crawlers responsible for generating the dungeon layout.
    /// </summary>
    public int numberofCrawlers;

    /// <summary>
    /// Minimum number of iterations for dungeon generation.
    /// </summary>
    public int iterationMin;

    /// <summary>
    /// Maximum number of iterations for dungeon generation.
    /// </summary>
    public int iterationMax;
}
