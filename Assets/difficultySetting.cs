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
    //speed to 3 6 7 9 for the rates of difficulty for the monster
    private void Awake()
    {
        // Ensure the GameManager persists between scenes
        DontDestroyOnLoad(gameObject);
    }
}
