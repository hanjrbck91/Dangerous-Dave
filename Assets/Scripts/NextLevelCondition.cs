using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelCondition : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Name of the next scene to load

    private bool shouldLoadNextScene = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            shouldLoadNextScene = true;
        }
    }

    private void Update()
    {
        if (shouldLoadNextScene)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
