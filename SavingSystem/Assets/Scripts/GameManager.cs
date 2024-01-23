using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IHasPersistentData
{
    public static GameManager Instance {get; private set;}

    Scenes currentScene;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }

    private void OnNewSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if(scene.name == Scenes.MainMenu.ToString())
        {
            Destroy(gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        currentScene = SceneLoader.lastLoadedScene;
    }

    public void SaveData(GameData data)
    {
        data.scene = currentScene;
    }
}
