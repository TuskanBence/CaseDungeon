using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for data persistence, defining methods for loading and saving game data.
/// </summary>
public interface IDataPersistence
{
    /// <summary>
    /// Loads game data from the provided data object.
    /// </summary>
    /// <param name="data">The GameData object containing the data to be loaded.</param>
    void LoadData(GameData data);

    /// <summary>
    /// Saves game data to the provided data object.
    /// </summary>
    /// <param name="data">The GameData object to which the data will be saved.</param>
    void SaveData(ref GameData data);
}
