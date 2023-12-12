using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

/// <summary>
/// Handles loading and saving game data to/from a file.
/// </summary>
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    /// <summary>
    /// Initializes a new instance of the FileDataHandler class.
    /// </summary>
    /// <param name="dataDirPath">The directory path where the data file is located.</param>
    /// <param name="dataFileName">The name of the data file.</param>
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    /// <summary>
    /// Loads game data from a file.
    /// </summary>
    /// <returns>The loaded GameData object, or null if the file does not exist or an error occurs.</returns>
    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error: " + e);
            }
        }
        return loadedData;
    }

    /// <summary>
    /// Saves game data to a file.
    /// </summary>
    /// <param name="data">The GameData object to be saved.</param>
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e);
        }
    }
}
