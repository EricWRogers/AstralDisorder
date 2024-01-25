using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Checkpoint : MonoBehaviour
{
    public bool shouldAITransition = true;
    public static Transform spawnpoint;
    private AIChaseState agent;
    private void Start()
    {
         agent = FindAnyObjectByType<AIChaseState>();
    }
    private void OnTriggerEnter(Collider other)
    {

        SaveManager.Instance.Save();
        spawnpoint = transform;
        OmnicatLabs.CharacterControllers.CharacterController.Instance.savedStamina = OmnicatLabs.CharacterControllers.CharacterController.Instance.currentStamina;
        //if (shouldAITransition)
        //    agent.checkPoint = true;
    }
}
