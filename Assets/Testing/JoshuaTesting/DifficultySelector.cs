using UnityEngine;
using UnityEngine.AI;

public class DifficultySelector : MonoBehaviour
{
    public DifficultySetting difficultySetting;
    public NavMeshAgent monsterNavMeshAgent; // Reference to the NavMeshAgent of the monster

    // Assign these methods to buttons in the UI
    public void SetEasyDifficulty()
    {
        difficultySetting.currentDifficulty = Difficulty.Easy;
        AdjustMonsterSpeed();
    }

    public void SetNormalDifficulty()
    {
        difficultySetting.currentDifficulty = Difficulty.Normal;
        AdjustMonsterSpeed();
    }

    public void SetHardDifficulty()
    {
        difficultySetting.currentDifficulty = Difficulty.Hard;
        AdjustMonsterSpeed();
    }

    public void SetExtremeDifficulty()
    {
        difficultySetting.currentDifficulty = Difficulty.Extreme;
        AdjustMonsterSpeed();
    }

    private void AdjustMonsterSpeed()
    {
        // Adjust the monster's speed based on the current difficulty
        switch (difficultySetting.currentDifficulty)
        {
            case Difficulty.Easy:
                monsterNavMeshAgent.speed = 5f; // Adjust this value accordingly
                break;
            case Difficulty.Normal:
                monsterNavMeshAgent.speed = 7f; // Adjust this value accordingly
                break;
            case Difficulty.Hard:
                monsterNavMeshAgent.speed = 9f; // Adjust this value accordingly
                break;
            case Difficulty.Extreme:
                monsterNavMeshAgent.speed = 12f; // Adjust this value accordingly
                break;
            default:
                monsterNavMeshAgent.speed = 7f; // Default speed
                break;
        }
    }
}
