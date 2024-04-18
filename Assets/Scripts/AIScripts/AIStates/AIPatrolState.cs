using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolState : MonoBehaviour, IEnemyState //Every state must inherit from here.
{
    private AIStateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject target;


    private int currentPatrolIndex = 0;
    private GameObject[] patrolRoutes;

    private AIChaseState chaseState;
    private Animator anim;

    public float agentPatrolSpeed = 5.0f;
    public void Enter(AIStateMachine stateMachine) //First thing the state does.
    {
        anim = GetComponent<AIStateMachine>().anim;
        Debug.Log("Entering Patrol State");
        this.stateMachine = stateMachine;
        //Debug.Log("Entering Idle State");
        //agent = GetComponentInParent<NavMeshAgent>();
        agent = gameObject.GetComponent<AIChaseState>().agent;
        target = gameObject.GetComponent<AIChaseState>().target;

        patrolRoutes = GameObject.FindGameObjectsWithTag("PatrolRoute");

        agent.speed = agentPatrolSpeed;

        chaseState = gameObject.GetComponent<AIChaseState>();

        anim.SetFloat("Slink", 1);
    }

    public void Run() //Good ol update
    {
        if(!chaseState.ValidatePath(target) && !chaseState.Attackable(target))
        {
            Patrol();
        }
        else
        {
            stateMachine.SetState(gameObject.GetComponent<AIChaseState>());
        }
    }

    public void Exit() //Last thing the state does before sending us wherever the user specified in update.
    {
        anim.SetFloat("Slink", 0);
        Debug.Log("Exiting Patrol State");
        agent.speed = DifficultySelector.Instance.currentSpeed;
    }


    public void Patrol()
    {

        Debug.Log("Patroling. .");


        if (patrolRoutes.Length > 0)
        {

            GameObject closestPatrolRoute = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject route in patrolRoutes)
            {

                float distanceToRoute = Vector3.Distance(target.transform.position, route.transform.position);

                if (distanceToRoute < closestDistance)
                {

                    closestDistance = distanceToRoute;
                    closestPatrolRoute = route;
                }
            }

            if (closestPatrolRoute != null)
            {

                Transform[] patrolPoints = closestPatrolRoute.GetComponentsInChildren<Transform>();


                List<Transform> validPatrolPoints = patrolPoints.Where(point => point != closestPatrolRoute.transform).ToList();

                if (validPatrolPoints.Count > 0)
                {

                    if (Vector3.Distance(agent.transform.position, validPatrolPoints[currentPatrolIndex].position) <= 2)
                    {
                        currentPatrolIndex = (currentPatrolIndex + 1) % validPatrolPoints.Count;
                    }

                    agent.SetDestination(validPatrolPoints[currentPatrolIndex].position);

                }
                else
                {
                    Debug.LogError("No valid patrol points found.");
                }
            }
            else
            {
                Debug.LogError("No valid patrol route found.");
            }
        }
        else
        {
            Debug.LogError("No patrol routes found in the scene.");
        }
    }
}
