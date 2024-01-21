using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GameData
{
    public int score;
    public float playerPosX, playerPosY, playerPosZ;
    [JsonIgnore] public Vector3 PlayerPos => new(playerPosX, playerPosY, playerPosZ);

    public GameData()
    {
        score = 0;
        playerPosX = playerPosY = playerPosZ = 0;
    }
}