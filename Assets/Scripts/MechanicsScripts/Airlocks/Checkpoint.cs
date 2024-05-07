using UnityEngine;
using UnityEngine.AI;

public class Checkpoint : MonoBehaviour
{
    public bool shouldAITransition = true;
    public static Transform spawnpoint;
    private AIChaseState agent;
    public Animator animator;
    public bool playerIsHere;
    public GameObject smokeParent;

    private void Start()
    {
         agent = FindAnyObjectByType<AIChaseState>();
    }
    private void OnTriggerEnter(Collider other)
    {
        SaveManager.Instance.Save();
        spawnpoint = transform;
        OmnicatLabs.CharacterControllers.CharacterController.Instance.savedStamina = OmnicatLabs.CharacterControllers.CharacterController.Instance.currentStamina;
        playerIsHere = true;
        animator.SetTrigger("Saving");
        //if (shouldAITransition)
        //    agent.checkPoint = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsHere = false;
    }
}
