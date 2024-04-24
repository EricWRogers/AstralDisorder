using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITransitionState : MonoBehaviour, IEnemyState //Every state must inherit from here.
{
    [HideInInspector]
    public AIStateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject target;

    public GameObject[] transitions;

    private Animator anim;
    public void Enter(AIStateMachine stateMachine) //First thing the state does.
    {
        anim = GetComponent<AIStateMachine>().anim;
        this.stateMachine = stateMachine;
        //Debug.Log("Entering Idle State");
        //agent = GetComponentInParent<NavMeshAgent>();
        agent = gameObject.GetComponent<AIChaseState>().agent;
        target = gameObject.GetComponent<AIChaseState>().target;

        transitions = GameObject.FindGameObjectsWithTag("Transition");
    }

    public void Run() //Good ol update
    {
        //Debug.Log("Entering Transition State");
        ResetAI(agent.gameObject, target);
       
    }

    public void Exit() //Last thing the state does before sending us wherever the user specified in update.
    {
        agent.stoppingDistance = 0.3f;
        agent.speed = DifficultySelector.Instance.currentSpeed;
        //Debug.Log("Exiting Transition State");

    }

    public void ResetAI(GameObject _ai, GameObject _player)
    {
        if (transitions.Length > 0)
        {
            GameObject closestTrans = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject trans in transitions)
            {
                NavMeshHit transHit, ai;
                float distanceToTrans = Vector3.Distance(_player.transform.position, trans.transform.position);
                NavMesh.SamplePosition(_ai.transform.position, out ai, 3.0f, NavMesh.AllAreas);
                NavMesh.SamplePosition(trans.transform.position, out transHit, 3.0f, NavMesh.AllAreas);
                if (distanceToTrans < closestDistance && transHit.mask == agent.areaMask)
                {
                    closestDistance = distanceToTrans;
                    closestTrans = trans;
                }
            }

            if (closestTrans != null)
            {
                NavMeshHit ai, player, closestTransHit;
                NavMesh.SamplePosition(_player.transform.position, out player, 10.0f, NavMesh.AllAreas);
                NavMesh.SamplePosition(_ai.transform.position, out ai, 10.0f, NavMesh.AllAreas);
                NavMesh.SamplePosition(closestTrans.transform.position, out closestTransHit, 10.0f, NavMesh.AllAreas);

                int currentAreaPlayer = player.mask;
                int currentAreaAI = ai.mask;
                int closestTransHitAreaHit = closestTransHit.mask;

                if (currentAreaAI != currentAreaPlayer /*&& closestTransHitAreaHit != currentAreaPlayer*/)
                {
                    //_ai.GetComponent<NavMeshAgent>().Warp(closestTrans.transform.position);

                    Debug.Log("Transition: " + closestTrans.name);
                   
                    agent.SetDestination(closestTrans.transform.position);
                    agent.stoppingDistance = 0f;
                    agent.speed = 10f;

                    float dis = Vector3.Distance(gameObject.transform.position, closestTrans.transform.position);
                    Debug.Log("Dis: " + dis);

                    if(dis < 0.1f)
                    {
                        Debug.Log("Transition: " + closestTrans.name);
                        Debug.Log("AI Pos: " + gameObject.transform.position + " teleport: " + closestTrans.transform.position);
                        stateMachine.SetState(gameObject.GetComponent<AIChaseState>());
                    }
                    
                }
                else
                {

                }
            }
        }

    }
}
