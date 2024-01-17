using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    GameData data;

    void Awake()
    {
        if(data == null)
            data = new GameData();    
    }

    // public static SaveGame()
    // {
        
    // }
}
