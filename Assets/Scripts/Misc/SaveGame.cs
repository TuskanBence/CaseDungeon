using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for triggering the save game process.
/// </summary>
public class SaveGame : MonoBehaviour
{
    /// <summary>
    /// Called when the "Save Game" button is clicked.
    /// Triggers the SaveGame method in the DataPresistenceManager.
    /// </summary>
    public void onClick()
    {
        // Trigger the SaveGame method in the DataPresistenceManager
        DataPresistenceManager.instance.SaveGame();
    }
}
