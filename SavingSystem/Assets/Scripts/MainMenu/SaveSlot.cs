using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI slotText;
    Button slotButton;
    public string slotID;

    void Awake()
    {
        slotButton = GetComponent<Button>();
    }

    // void Start()
    // {
    //     slotButton.onClick.AddListener(() => 
    //     {
    //         DataPersistenceManager
    //     });
    // }

    public void SetData(GameData data)
    {
        if(data == null)
        {
            slotText.text = "Empty Slot";
        }
        else
        {
            slotText.text = $"Score: {data.score}";
        }
    }

    public void StartGame()
    {
        DataPersistenceManager.Instance.StartGame(slotID);
    }
}
