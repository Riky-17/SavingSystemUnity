using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsManager : MonoBehaviour
{
    public static SaveSlotsManager Instance {get; private set;}

    [SerializeField] GameObject firstScreenUI;
    List<SaveSlot> saveSlots;

    SaveSlot slotToCopyFrom;
    SaveSlot slotToCopyTo;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        saveSlots = new(GetComponentsInChildren<SaveSlot>());
        UpdateSlotsData();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            slotToCopyFrom = null;
            firstScreenUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void UpdateSlotsData()
    {
        Dictionary<string, GameData> allData = DataPersistenceManager.Instance.GetAlldata();
        foreach (SaveSlot saveSlot in saveSlots)
        {
            allData.TryGetValue(saveSlot.slotID, out GameData slotData);
            saveSlot.SetData(slotData);
        }
    }

    public void SetSlotToCopyFrom(SaveSlot slotFrom)
    {
        slotToCopyFrom = slotFrom;
        foreach (SaveSlot slot in saveSlots)
        {
            slot.RemoveListenersToButtons();
            slot.slotButton.onClick.AddListener(slot.CopyTo);
        }
    }

    public void SetSlotToCopyTo(SaveSlot slotTo)
    {
        slotToCopyTo = slotTo;
        if(slotToCopyFrom != slotToCopyTo)
            DataPersistenceManager.Instance.CopyData(slotToCopyFrom.slotID, slotToCopyTo.slotID);
        
        UpdateSlotsData();

        foreach (SaveSlot slot in saveSlots)
        {
            slot.RemoveListenersToButtons();
            slot.SetListenersToButtons();
        }

        slotToCopyFrom = slotToCopyTo = null;
    }
}
