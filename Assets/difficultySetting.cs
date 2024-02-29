using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Extreme
}

public class DifficultySetting : MonoBehaviour
{
    public Difficulty currentDifficulty = Difficulty.Normal;

    // Any other properties or methods related to difficulty setting can be added here

    private void Awake()
    {
        // Ensure the GameManager persists between scenes
        DontDestroyOnLoad(gameObject);
    }
}
