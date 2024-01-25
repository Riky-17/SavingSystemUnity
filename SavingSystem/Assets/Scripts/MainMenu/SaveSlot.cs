using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI slotText;
    public Button slotButton {get; private set;}
    GameData slotData;
    [SerializeField] Button deleteButton;
    [SerializeField] Button copyButton;
    public string slotID;

    void OnEnable()
    {
        slotButton = GetComponent<Button>();
        RemoveListenersToButtons();
        SetListenersToButtons();
    }

    public void SetData(GameData data)
    {
        if(data == null)
        {
            deleteButton.gameObject.SetActive(false);
            copyButton.gameObject.SetActive(false);
            slotData = null;
            slotText.text = "Empty Slot";
        }
        else
        {
            deleteButton.gameObject.SetActive(true);
            copyButton.gameObject.SetActive(true);
            slotData = data;
            slotText.text = $"Score: {data.score} " + string.Concat(data.scene.ToString().Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }

    void DeleteData()
    {
        DataPersistenceManager.Instance.DeleteData(slotID);
        SaveSlotsManager.Instance.UpdateSlotsData();
    }

    void CopyFrom()
    {
        SaveSlotsManager.Instance.SetSlotToCopyFrom(this);
    }

    public void CopyTo()
    {
        SaveSlotsManager.Instance.SetSlotToCopyTo(this);
    }

    public void SetListenersToButtons()
    {
        slotButton.onClick.AddListener(StartGame);
        copyButton.onClick.AddListener(CopyFrom);
        deleteButton.onClick.AddListener(DeleteData);
    }

    public void RemoveListenersToButtons()
    {
        slotButton.onClick.RemoveAllListeners();
        copyButton.onClick.RemoveAllListeners();
        deleteButton.onClick.RemoveAllListeners();
    }

    public void StartGame()
    {
        DataPersistenceManager.Instance.StartGame(slotID, slotData);
    }
}
