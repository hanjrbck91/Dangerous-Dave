using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private GameObject restartPanel;

    private bool isRestartConfirmationActive = false;
    private bool isQuitConfirmationActive = false;

    private void Update()
    {
        // Check if F1 key is pressed
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleGameObject(helpPanel);
        }

        // Check if F2 key is pressed
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleGameObject(controlPanel);
        }

        // Check if F3 key is pressed
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ToggleGameObject(restartPanel);
            isRestartConfirmationActive = !isRestartConfirmationActive;
        }

        // Check if F9 key is pressed
        if (Input.GetKeyDown(KeyCode.F9))
        {
            TogglePause();
        }

        // Check if F10 key or Escape key is pressed
        if (Input.GetKeyDown(KeyCode.F10) || Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameObject(quitPanel);
            isQuitConfirmationActive = !isQuitConfirmationActive;
        }

        // Check for restart confirmation
        if (isRestartConfirmationActive)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                RestartGame();
                isRestartConfirmationActive = false;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                ToggleGameObject(restartPanel);
                isRestartConfirmationActive = false;
            }
        }

        // Check for quit confirmation
        if (isQuitConfirmationActive)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                QuitGame();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                ToggleGameObject(quitPanel);
                isQuitConfirmationActive = false;
            }
        }
    }

    private void ToggleGameObject(GameObject go)
    {
        if (go != null)
        {
            go.SetActive(!go.activeSelf);
        }
    }

    private void RestartGame()
    {
        // Implement your game restart logic here
        // For example:
        SceneManager.LoadScene("Level-1");
        Time.timeScale = 1; // Reset time scale to resume normal gameplay speed
    }


    private void TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        ToggleGameObject(pausePanel);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is ended");
    }
}
