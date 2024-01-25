using System;
using UnityEngine;

public class Collectible : MonoBehaviour, IHasPersistentData
{
    [SerializeField] string collectibleID;
    bool isCollected = false;

    public void Collect()
    {
        isCollected = true;
        gameObject.SetActive(!isCollected);
    }

    public void LoadData(GameData data)
    {
        data.collectibles.TryGetValue(collectibleID, out isCollected);
        gameObject.SetActive(!isCollected);
    }

    public void SaveData(GameData data)
    {
        if(data.collectibles.ContainsKey(collectibleID))
            data.collectibles.Remove(collectibleID);

        data.collectibles.Add(collectibleID, isCollected);
    }

    [ContextMenu("Generate ID")]
    void GenerateID()
    {
        collectibleID = Guid.NewGuid().ToString();
    }
}
