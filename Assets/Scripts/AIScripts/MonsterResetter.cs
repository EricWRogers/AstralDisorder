using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterResetter : MonoBehaviour
{
    public Transform resetPoint;
    private void Start()
    {
        SaveManager.Instance.onReset.AddListener(Reposition);
    }

    private void Reposition()
    {
        //var transState = GetComponentInChildren<AITransitionState>();
        //Debug.Log(transState + "HEY");
        GetComponent<NavMeshAgent>().areaMask = 1 << NavMesh.GetAreaFromName("Train Yard");
        GetComponent<NavMeshAgent>().Warp(resetPoint.position);
	
    }
}
