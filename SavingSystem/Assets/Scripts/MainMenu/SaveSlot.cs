using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI slotText;
    GameData slotData;
    //Button slotButton;
    public string slotID;

    // void Awake()
    // {
    //     slotButton = GetComponent<Button>();
    // }

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
            slotData = data;
            slotText.text = $"Score: {data.score} " + string.Concat(data.scene.ToString().Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }

    public void StartGame()
    {
        DataPersistenceManager.Instance.StartGame(slotID, slotData);
    }
}
