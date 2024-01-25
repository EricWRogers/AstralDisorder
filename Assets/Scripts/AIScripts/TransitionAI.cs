using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransitionAI : MonoBehaviour
{
    public Transform exitTrans;

    GameObject agent;
    private void Start()
    {
         agent = FindAnyObjectByType<NavMeshAgent>().gameObject;
    }
   /* private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && agent.GetComponentInChildren<AIMoveState>().checkPoint == true)
        {
            Debug.Log("Crossed");
            // GameObject temp = GetComponentInParent<NavMeshAgent>().gameObject;
            
            agent.SetActive(false);
            agent.transform.position = exitTrans.position;
            agent.SetActive(true);
            agent.GetComponentInChildren<AIMoveState>().checkPoint = false;
        }
    }*/
}
