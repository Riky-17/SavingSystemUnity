using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Scenes sceneToLoad;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            DataPersistenceManager.Instance.SaveGameData();
            SceneLoader.LoadScene(sceneToLoad);
        }
    }
}
