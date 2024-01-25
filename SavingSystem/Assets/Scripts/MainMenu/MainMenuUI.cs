using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject SaveslotsUI;

    public void StartGameButton()
    {
        SaveslotsUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
