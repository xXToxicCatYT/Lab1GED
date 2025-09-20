using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static reference to the single instance
    public static GameManager Instance { get; private set; }

    public int score = 0;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes
    }

    public void AddDeathScore(int points)
    {
        score += points;
        Debug.Log("Current Deaths: " + score);
    }
}

