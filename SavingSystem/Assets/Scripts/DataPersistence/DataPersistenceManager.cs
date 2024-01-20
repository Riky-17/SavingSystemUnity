using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance {get; private set;}

    public GameData data {get; private set;}
    FileManager fileManager;
    List<IHasPersistentData> persistentDataObjs;

    void Awake()
    {
        Instance = this;

        // creating new file manager
        fileManager ??= new(Application.persistentDataPath, "test");
        
        //searching for objects that have data to save or load
        persistentDataObjs = GetPersistentDataObjs();

        LoadGame();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            SaveGame();
    }

    void SaveGame()
    {
        foreach (IHasPersistentData obj in persistentDataObjs)
        {
            obj.SaveData(data);
        }
        fileManager.SaveData(data);
    }

    void LoadGame()
    {
        data = fileManager.LoadData();
        //if data is null we create a new game data
        data ??= new();
        foreach (IHasPersistentData obj in persistentDataObjs)
        {
            obj.LoadData(data);
        }
    }

    List<IHasPersistentData> GetPersistentDataObjs()
    {
        return new(FindObjectsOfType<MonoBehaviour>().OfType<IHasPersistentData>());
    }
}
