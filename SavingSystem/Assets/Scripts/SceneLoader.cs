using UnityEngine.SceneManagement;

public enum Scenes 
{
    MainMenu,
    FirstLevel,
    SecondLevel
}

public static class SceneLoader
{

    public static Scenes lastLoadedScene {get; private set;}

    public static void LoadScene(Scenes sceneToLoad)
    {
        lastLoadedScene = sceneToLoad;
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}