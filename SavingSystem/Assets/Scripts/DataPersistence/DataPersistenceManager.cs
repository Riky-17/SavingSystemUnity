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
        fileManager ??= new(Application.persistentDataPath, "SaveFile");
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

    public void StartGame(string slotID, GameData slotData)
    {
        currentSlot = slotID;
        data = slotData ?? new();
        SceneLoader.LoadScene(data.scene);
    }

    void SaveGame()
    {
        SaveGameData();
        fileManager.SaveData(data, currentSlot);
    }

    public void SaveGameData()
    {
        foreach (IHasPersistentData obj in persistentDataObjs)
            obj.SaveData(data);
    }

    void LoadGameData()
    {
        foreach (IHasPersistentData obj in persistentDataObjs)
            obj.LoadData(data);
    }

    public void DeleteData(string slotID)
    {
        fileManager.DeleteData(slotID);
    }

    public void CopyData(string slotIDFrom, string slotIDTo)
    {
        fileManager.CopyData(slotIDFrom, slotIDTo);
    }

    public void LoadMainMenuScene()
    {
        currentSlot = null;
        data = null;
        SceneLoader.LoadScene(Scenes.MainMenu);
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
        
        LoadGameData();
    }
}