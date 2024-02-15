using UnityEngine;
using UnityEngine.AI;

public class AIAttackState : MonoBehaviour, IEnemyState
{
    private AIStateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject target;
    private AIChaseState chase;

    private GameStateController controller;
    public int attackRange = 10;

    private bool lose;
    private Animator anim;
    public void Enter(AIStateMachine stateMachine) //First thing the state does.
    {
        anim = GetComponent<AIStateMachine>().anim;
        //Debug.Log("Entering Attack State");
        this.stateMachine = stateMachine;

        agent = gameObject.GetComponent<AIChaseState>().agent;
        target = gameObject.GetComponent<AIChaseState>().target;

        chase = gameObject.GetComponent<AIChaseState>();

        controller = GameStateController.Instance;
        lose = false;

        anim.SetFloat("Sprint", 1);
    }

    public void Run() //Good ol update
    {
        agent.SetDestination(target.transform.position);

        if (!lose && !chase.ValidatePath(target) && Vector3.Distance(chase.lastHit.position, target.transform.position) <= attackRange)
        {
            if (Vector3.Distance(agent.transform.position, target.transform.position) <= attackRange)
            {
                anim.SetFloat("Sprint", 0);
                anim.SetFloat("Scream", 1);
                controller.ActivateLose();
                lose = true;
                stateMachine.SetState(gameObject.GetComponent<AIIdleState>());
            }
        }
        else
        {
            stateMachine.SetState(gameObject.GetComponent<AIChaseState>());
        }
    }
    public void Exit() //Last thing the state does before sending us wherever the user specified in update.
    {
        anim.SetFloat("Scream", 0);
        //Debug.Log("Exiting Attack State");
    }
}
