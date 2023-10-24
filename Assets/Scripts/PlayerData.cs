using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static PlayerData instance;
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerData>();
                if (instance == null)
                {
                    instance = new GameObject("PlayerData").AddComponent<PlayerData>();
                }
            }
            return instance;
        }
    }

    private int points = 0;
    public int GetPoints() => points;

    // ... Other methods ...

    public void AddPoints(int amount)
    {
        points += amount;
    }

    public void SavePoints()
    {
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.Save(); // Explicitly save PlayerPrefs changes
    }

    public void LoadPoints()
    {
        points = PlayerPrefs.GetInt("Points", 0);
    }
}
