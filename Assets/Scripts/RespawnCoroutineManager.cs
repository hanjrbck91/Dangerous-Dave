using System.Collections;
using UnityEngine;

public class RespawnCoroutineManager : MonoBehaviour
{
    private static RespawnCoroutineManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy the new instance
            Destroy(gameObject);
        }
    }

    public void StartRespawnCoroutine(PlayerManager playerManager)
    {
        // Check if the instance is not null before starting the coroutine
        if (instance != null)
        {
            StartCoroutine(playerManager.RespawnPlayer());
        }
    }
}
