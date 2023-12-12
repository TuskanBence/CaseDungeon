using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Class responsible for deleting a save file.
/// </summary>
public class DeleteSave : MonoBehaviour
{
    /// <summary>
    /// Deletes the save file if it exists.
    /// </summary>
    public void deleteSave()
    {
        // Construct the full path to the save file
        string fullPath = Path.Combine(Application.persistentDataPath, "saveGame.game");

        // Check if the file exists
        if (File.Exists(fullPath))
        {
            // File exists, so delete it
            File.Delete(fullPath);
            Debug.Log("File deleted successfully.");
        }
        else
        {
            // File does not exist, cannot delete
            Debug.Log("File does not exist, cannot delete.");
        }
    }
}
