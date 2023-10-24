using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Name of the next scene to load
    [SerializeField] private float transitionDelay = 7.5f; // Delay in seconds before loading the next scene

    private float currentTime = 0f;
    private bool shouldLoadNextScene = false;

    private void Update()
    {
        // Start counting time once the scene is loaded
        if (!shouldLoadNextScene)
        {
            currentTime += Time.deltaTime;

            // Check if the specified delay has passed
            if (currentTime >= transitionDelay)
            {
                shouldLoadNextScene = true;
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
