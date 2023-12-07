using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class DataPresistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gamedata;
    private List<IDataPersistence> dataPresistencesObjects;
    private FileDataHandler dataHandler;
  public static DataPresistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of DataPresistenceManager");
            Destroy(this.gameObject);
            return;
        }
       
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPresistencesObjects = FindAllDataPresisnteceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPresisnteceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        this.gamedata=new GameData();
    }
    public void LoadGame()
    {
        this.gamedata = dataHandler.Load();
        if (this.gamedata==null)
        {
            Debug.Log("No data found dffauk");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceobj in dataPresistencesObjects)
        {
            dataPersistenceobj.LoadData(gamedata);
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceobj in dataPresistencesObjects)
        {
            dataPersistenceobj.SaveData(ref gamedata);
        }
        dataHandler.Save(gamedata);
    }
    public void OnApplicationQuit()
    {
        SaveGame();
    }
}
