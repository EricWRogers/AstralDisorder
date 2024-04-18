using UnityEngine;
using UnityEngine.AI;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Extreme
}

public class DifficultySelector : MonoBehaviour
{
    public NavMeshAgent monsterNavMeshAgent; // Reference to the NavMeshAgent of the monster
    public float easySpeed = 3.0f;
    public float mediumSpeed = 6.0f;
    public float hardSpeed = 7.0f;
    public float extremeSpeed = 9.0f;
    [HideInInspector]
    public float currentSpeed;
    public Difficulty currentDifficulty;

    public static DifficultySelector Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Assign these methods to buttons in the UI
    public void SetEasyDifficulty()
    {
        monsterNavMeshAgent.speed = easySpeed;
        currentSpeed = easySpeed;
        currentDifficulty = Difficulty.Easy;
    }

    public void SetNormalDifficulty()
    {
        monsterNavMeshAgent.speed = mediumSpeed;
        currentSpeed = mediumSpeed;
        currentDifficulty = Difficulty.Normal;
    }

    public void SetHardDifficulty()
    {
        monsterNavMeshAgent.speed = hardSpeed;
        currentSpeed = hardSpeed;
        currentDifficulty = Difficulty.Hard;
    }

    public void SetExtremeDifficulty()
    {
        monsterNavMeshAgent.speed = extremeSpeed;
        currentSpeed = extremeSpeed;
        currentDifficulty = Difficulty.Extreme;
    }
}
