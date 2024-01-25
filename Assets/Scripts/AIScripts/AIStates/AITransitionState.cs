using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITransitionState : MonoBehaviour, IEnemyState //Every state must inherit from here.
{
    private AIStateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject target;

    private GameObject[] transitions;

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
        stateMachine.SetState(gameObject.GetComponent<AIChaseState>());
    }

    public void Exit() //Last thing the state does before sending us wherever the user specified in update.
    {
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
                float distanceToTrans = Vector3.Distance(target.transform.position, trans.transform.position);

                if (distanceToTrans < closestDistance)
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

                if (currentAreaAI != currentAreaPlayer && closestTransHitAreaHit == currentAreaPlayer)
                {
                    _ai.GetComponent<NavMeshAgent>().Warp(closestTrans.transform.position);
                }
            }
        }

    }
}
