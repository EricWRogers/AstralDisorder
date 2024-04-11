using UnityEngine;
using UnityEngine.AI;

public class DifficultySelector : MonoBehaviour
{
    public NavMeshAgent monsterNavMeshAgent; // Reference to the NavMeshAgent of the monster
    public float easySpeed = 3.0f;
    public float mediumSpeed = 6.0f;
    public float hardSpeed = 7.0f;
    public float extremeSpeed = 9.0f;

    // Assign these methods to buttons in the UI
    public void SetEasyDifficulty()
    {
        monsterNavMeshAgent.speed = easySpeed;
    }

    public void SetNormalDifficulty()
    {
        monsterNavMeshAgent.speed = mediumSpeed;
    }

    public void SetHardDifficulty()
    {
        monsterNavMeshAgent.speed = hardSpeed;
    }

    public void SetExtremeDifficulty()
    {
        monsterNavMeshAgent.speed = extremeSpeed;
    }
}
