using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of GameManager exists
    public static GameManager instance;

    // Difficulty settings
    public int maxHealthIncrease = 0;
    public float moveSpeedIncrease = 0f;
    public int damageAmountMultiplier = 1;
    public GameObject zombiePrefab;

    private void Awake()
    {
        // Implementing the Singleton pattern
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

    // Function to increase difficulty
    public void IncreaseDifficulty()
    {
        // Adjust the difficulty settings
        maxHealthIncrease += 1;
        moveSpeedIncrease += 1f;
        damageAmountMultiplier +=1;
    }
}

