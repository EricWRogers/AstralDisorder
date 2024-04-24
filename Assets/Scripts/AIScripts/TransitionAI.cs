using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransitionAI : MonoBehaviour
{
    public Transform exitTrans;
    public string area2;

    GameObject agent;
    private void Start()
    {
        agent = FindAnyObjectByType<NavMeshAgent>().gameObject;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && agent.GetComponentInChildren<AIStateMachine>().currentState is AITransitionState)
        {
            Debug.Log("Crossed");
            // GameObject temp = GetComponentInParent<NavMeshAgent>().gameObject;
            
            agent.GetComponent<NavMeshAgent>().Warp(exitTrans.position);
            agent.GetComponent<NavMeshAgent>().areaMask = NavMesh.GetAreaFromName(area2);
            //agent.transform.position = exitTrans.position;
            
            
        }
    }
}
