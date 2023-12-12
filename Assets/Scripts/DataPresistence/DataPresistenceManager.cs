using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.IO;

/// <summary>
/// Manages data persistence in the game, including saving and loading game data.
/// </summary>
public class DataPresistenceManager : MonoBehaviour
{
    /// <summary>
    /// The name of the file used for storing game data.
    /// </summary>
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    /// <summary>
    /// The current game data being managed by the manager.
    /// </summary>
    private GameData gamedata;

    /// <summary>
    /// Objects implementing the IDataPersistence interface in the scene.
    /// </summary>
    private List<IDataPersistence> dataPresistencesObjects;

    /// <summary>
    /// Handles file-based load and saveing.
    /// </summary>
    private FileDataHandler dataHandler;

    /// <summary>
    /// Static reference to the DataPresistenceManager instance.
    /// </summary>
    public static DataPresistenceManager instance { get; private set; }

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of DataPresistenceManager");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPresistencesObjects = FindAllDataPresisnteceObjects();
        LoadGame();
    }

    /// <summary>
    /// Finds all objects implementing the IDataPersistence interface in the scene.
    /// </summary>
    /// <returns>A list of objects implementing the IDataPersistence interface.</returns>
    private List<IDataPersistence> FindAllDataPresisnteceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    /// <summary>
    /// Starts a new game by creating a new GameData instance.
    /// </summary>
    public void NewGame()
    {
        this.gamedata = new GameData();
    }

    /// <summary>
    /// Loads game data from file and distributes it to objects implementing the IDataPersistence interface.
    /// </summary>
    public void LoadGame()
    {
        this.gamedata = dataHandler.Load();
        if (this.gamedata == null)
        {
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceobj in dataPresistencesObjects)
        {
            dataPersistenceobj.LoadData(gamedata);
        }
    }

    /// <summary>
    /// Saves game data by collecting data from objects implementing the IDataPersistence interface.
    /// </summary>
    public void SaveGame()
    {
        Debug.Log("Saving game data...");
        this.dataPresistencesObjects = FindAllDataPresisnteceObjects();
        foreach (IDataPersistence dataPersistenceobj in dataPresistencesObjects)
        {
            dataPersistenceobj.SaveData(ref gamedata);
            Debug.Log(dataPersistenceobj + " saved.");
        }
        dataHandler.Save(gamedata);
        Debug.Log("Game data saved.");
    }
}
