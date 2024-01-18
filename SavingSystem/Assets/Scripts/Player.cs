using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<int> OnScoreGained; 
    float speed = 5f;
    int score = 0;

    void Start()
    {
        score = DataPersistanceManager.Instance.data.score;
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

        if(Input.GetKeyDown(KeyCode.N))
            DataPersistanceManager.Instance.SaveGame(score);
    }
}
