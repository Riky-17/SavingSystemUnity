using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsManager : MonoBehaviour
{
    [SerializeField] GameObject firstScreenUI;
    List<SaveSlot> saveSlots;

    void OnEnable()
    {
        saveSlots = new(GetComponentsInChildren<SaveSlot>());
        Dictionary<string, GameData> allData = DataPersistenceManager.Instance.GetAlldata();
        foreach (SaveSlot saveSlot in saveSlots)
        {
            allData.TryGetValue(saveSlot.slotID, out GameData slotData);
            saveSlot.SetData(slotData);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            firstScreenUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
