using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeleteSave : MonoBehaviour
{
   public void deleteSave()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "saveGame.game");

        if (File.Exists(fullPath))
        {
            // File exists, so delete it
            File.Delete(fullPath);
            Debug.Log("File deleted successfully.");
        }
        else
        {
            Debug.Log("File does not exist, cannot delete.");
        }
    }
}
