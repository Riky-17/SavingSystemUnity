using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance {get; private set;}

    GameData data;
    string currentSlot;
    FileManager fileManager;
    List<IHasPersistentData> persistentDataObjs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // creating new file manager
        fileManager ??= new(Application.persistentDataPath, "test");
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            SaveGame();
    }

    public void StartGame(string slotID)
    {
        currentSlot = slotID;
        LoadGameScene();
    }

    void SaveGame()
    {
        SaveGameData();
        fileManager.SaveData(data, currentSlot);
    }

    void SaveGameData()
    {
        foreach (IHasPersistentData obj in persistentDataObjs)
        {
            obj.SaveData(data);
        }
    }

    void LoadGame()
    {
        data = fileManager.LoadData(currentSlot);
        //if data is null we create a new game data
        data ??= new();
        foreach (IHasPersistentData obj in persistentDataObjs)
        {
            obj.LoadData(data);
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMainMenuScene()
    {
        currentSlot = null;
        SceneManager.LoadScene("MainMenu");
    }

    List<IHasPersistentData> GetPersistentDataObjs()
    {
        return new(FindObjectsOfType<MonoBehaviour>().OfType<IHasPersistentData>());
    }

    public Dictionary<string, GameData> GetAlldata()
    {
        return fileManager.GetAllData();
    }

    private void OnNewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(currentSlot == null)
            return;
            
        //searching for objects that have data to save or load
        persistentDataObjs = GetPersistentDataObjs();
        
        LoadGame();
    }
}