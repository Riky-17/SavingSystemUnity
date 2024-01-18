using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance {get; private set;}

    public GameData data {get; private set;}
    FileManager fileManager;

    void Awake()
    {
        Instance = this;
        fileManager ??= new(Application.persistentDataPath, "test");    
        data = fileManager.LoadData();
        data ??= new();    
    }

    public void SaveGame(int score)
    {
        data.score = score;
        fileManager.SaveData(data);
    }
}
