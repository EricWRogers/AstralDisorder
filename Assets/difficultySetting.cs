using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Extreme
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Difficulty currentDifficulty = Difficulty.Normal;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Difficulty GetDifficulty()
    {
        return currentDifficulty;
    }

    // Other methods and properties as needed
}
