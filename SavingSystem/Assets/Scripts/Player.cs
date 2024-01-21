using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHasPersistentData
{
    public static event Action<int> OnScoreGained; 
    float speed = 5f;
    int score = 0;

    void Start()
    {
        OnScoreGained?.Invoke(score);
    }

    void Update()
    {
        Vector3 moveDir = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
            moveDir += Vector3.forward;
        if(Input.GetKey(KeyCode.S))
            moveDir += -Vector3.forward;
        if(Input.GetKey(KeyCode.D))
            moveDir += Vector3.right;
        if(Input.GetKey(KeyCode.A))
            moveDir += -Vector3.right;

        moveDir = moveDir.normalized;
        transform.position += speed * Time.deltaTime * moveDir;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            score++;
            OnScoreGained?.Invoke(score);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            DataPersistenceManager.Instance.LoadMainMenuScene();
    }

    public void SaveData(GameData data)
    {
        data.score = score;
        data.playerPosX = transform.position.x;
        data.playerPosY = transform.position.y;
        data.playerPosZ = transform.position.z;
    }

    public void LoadData(GameData data)
    {
        score = data.score;
        OnScoreGained?.Invoke(score);
        transform.position = data.PlayerPos;
    }
}
