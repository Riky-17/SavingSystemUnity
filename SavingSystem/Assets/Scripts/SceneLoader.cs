using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes 
{
    MainMenu,
    FirstLevel,
    SecondLevel
}

public static class SceneLoader
{
    public static void LoadScene(Scenes sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}