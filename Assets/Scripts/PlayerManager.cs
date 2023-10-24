using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private int points = 0;
    private int lives = 4;

    public GameObject numberDisplayParent; // Reference to the parent GameObject holding number digit images
    public GameObject[] Digits;

    public GameObject healthDisplayParent;
    public GameObject[] healthIndicatorSprites;

    public GameObject CupWinDisplay;
    public GameObject GunDisplay;
    public GameObject JetDisplay;
    public GameObject GameOverDisplay; // Reference to the Game Over GameObject

    private bool cupPointsAdded = false; // Track if cup points have been added

    [SerializeField] private AudioSource gemSound;
    [SerializeField] private AudioSource cupSound;
    [SerializeField] private AudioSource gunSound;
    [SerializeField] private AudioSource jetDisplaySound;
    [SerializeField] private AudioSource Deathsound;

    [SerializeField] private GameObject doorCollider;

    private RespawnCoroutineManager coroutineManager; // Reference to the CoroutineManager
    private Vector3 initialPosition; // Store the initial position of the player

    private PlayerData playerData; // Reference to the PlayerData singleton


    private void Start()
    {
        // Initialize health indicator sprites array
        healthIndicatorSprites = new GameObject[lives];
        for (int i = 0; i < lives; i++)
        {
            healthIndicatorSprites[i] = healthDisplayParent.transform.GetChild(i).gameObject;
        }

        // Load points from PlayerData
        points = PlayerData.Instance.GetPoints()/2;
        Debug.Log("Points loaded: " + points); // Debugging statement

        coroutineManager = FindObjectOfType<RespawnCoroutineManager>(); // Find the RespawnCoroutineManager in the scene
        initialPosition = transform.position;
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SkyBlueGem") && !collision.gameObject.GetComponent<CollectedGem>())
        {
            Destroy(collision.gameObject);
            collision.gameObject.AddComponent<CollectedGem>();
            AddPoints(100);
            gemSound.Play();
            Debug.Log("Cup Won! Final Score: " + points + " points");
        }
        else if (collision.CompareTag("VioletGem") && !collision.gameObject.GetComponent<CollectedGem>())
        {
            Destroy(collision.gameObject);
            collision.gameObject.AddComponent<CollectedGem>();
            AddPoints(50);
            gemSound.Play();
            Debug.Log("Cup Won! Final Score: " + points + " points");
        }
        else if (collision.CompareTag("RedGem") && !collision.gameObject.GetComponent<CollectedGem>())
        {
            Destroy(collision.gameObject);
            collision.gameObject.AddComponent<CollectedGem>();
            AddPoints(150);
            gemSound.Play();
            Debug.Log("Cup Won! Final Score: " + points + " points");
        }
        else if (collision.CompareTag("Ring") && !collision.gameObject.GetComponent<CollectedGem>())
        {
            Destroy(collision.gameObject);
            collision.gameObject.AddComponent<CollectedGem>();
            AddPoints(200);
            gemSound.Play();
            Debug.Log("Cup Won! Final Score: " + points + " points");
        }
        else if (collision.CompareTag("Crown") && !collision.gameObject.GetComponent<CollectedGem>())
        {
            Destroy(collision.gameObject);
            collision.gameObject.AddComponent<CollectedGem>();
            AddPoints(300);
            gemSound.Play();
            Debug.Log("Cup Won! Final Score: " + points + " points");
        }
        else if (collision.CompareTag("GreenGem") && !collision.gameObject.GetComponent<CollectedGem>())
        {
            Destroy(collision.gameObject);
            collision.gameObject.AddComponent<CollectedGem>();
            AddPoints(500);
            gemSound.Play();
            Debug.Log("Cup Won! Final Score: " + points + " points");
        }
        else if (collision.CompareTag("Cup") && !cupPointsAdded)
        {
            Destroy(collision.gameObject);
            collision.gameObject.AddComponent<CollectedGem>();
            AddPoints(1000);
            cupPointsAdded = true; // Set the flag to true
            CupWinDisplay.SetActive(true);
            doorCollider.SetActive(false);
            cupSound.Play();
            Debug.Log("Cup Won! Final Score: " + points + " points");
        }
        else if (collision.CompareTag("Door") && cupPointsAdded)
        {
            // Save points to PlayerPrefs before transitioning to the next level
            SavePointsToPlayerPrefs();
            Debug.Log("Points saved: " + points); // Debugging statement

            Debug.Log("Next level available.");


        }
        else if (collision.CompareTag("Gun"))
        {
            Destroy(collision.gameObject);
            GunDisplay.SetActive(true);
            gunSound.Play();
            Debug.Log("Gun Added");
        }
        else if (collision.CompareTag("Jet"))
        {
            Destroy(collision.gameObject);
            JetDisplay.SetActive(true);
            jetDisplaySound.Play();
            Debug.Log("Jet Added");
        }
        else if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Fire"))
        {
            // Instantiate the death tile at the player's position
            string deathTilePrefabPath = "Prefabs/DeathTilePrefab"; // Adjust the path based on your project structure
            GameObject deathTilePrefab = Resources.Load<GameObject>(deathTilePrefabPath);

            if (deathTilePrefab != null)
            {
                Debug.Log("Prefab loaded successfully.");
                GameObject deathTile = Instantiate(deathTilePrefab, transform.position, Quaternion.identity);

                // Destroy the death tile after 2 seconds
                Destroy(deathTile, 2f);
            }
            else
            {
                Debug.LogError("Prefab not found at path: " + deathTilePrefabPath);
            }

            DeactivatePlayer(); // Deactivate the player
            Deathsound.Play();
            Debug.Log("Player Destroyed by Enemy Bullet");

            if (coroutineManager != null)
            {
                coroutineManager.StartRespawnCoroutine(this);
            }

            // Reduce player lives by 1
            lives--;

            // Check if the player has remaining lives
            if (lives > 0)
            {
                // Handle player respawn
                // ...
                UpdateUI();
            }
            else
            {
                // No more lives left, activate Game Over
                Time.timeScale = 0; // Pause the game
                DeactivatePlayer();
                GameOverDisplay.SetActive(true); // Activate the Game Over GameObject
                Debug.Log("Game Over");
            }
        }
        UpdateUI();
    }

    private void SavePointsToPlayerPrefs()
    {
        int pointsToSave = points;  // Save half of the points
        PlayerData.Instance.AddPoints(pointsToSave); // Add current level's points to PlayerData
        PlayerData.Instance.SavePoints(); // Save points to PlayerPrefs
    }


    // Coroutine to respawn the player after a delay
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        ActivatePlayer(); // Activate the player
        transform.position = initialPosition; // Reset the player position
    }

    // Deactivate the player
    private void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }

    // Activate the player
    private void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    private void UpdateUI()
    {
        Debug.Log("Updating UI with points: " + points); // Debugging statement
        UpdateNumberDisplay(points); // Update the number display for points

        // Update health indicator display
        for (int i = 0; i < healthIndicatorSprites.Length; i++)
        {
            if (i < lives)
            {
                healthIndicatorSprites[i].SetActive(true); // Activate sprite
            }
            else
            {
                healthIndicatorSprites[i].SetActive(false); // Deactivate sprite
            }
        }
    }

    private void UpdateNumberDisplay(int number)
    {
        // Deactivate all digit GameObjects first
        foreach (GameObject digitParent in Digits)
        {
            foreach (Transform child in digitParent.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        int DigitPosition = 0; // Start from the lowest place value (units)

        while (number > 0 && DigitPosition < Digits.Length)
        {
            int digitNumber = number % 10;
            number = number / 10;

            if (digitNumber >= 0 && digitNumber < Digits[DigitPosition].transform.childCount)
            {
                Digits[DigitPosition].transform.GetChild(digitNumber).gameObject.SetActive(true);
            }

            DigitPosition++;
        }
    }
}