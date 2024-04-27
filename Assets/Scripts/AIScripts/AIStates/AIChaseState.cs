using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIChaseState : MonoBehaviour, IEnemyState
{
    private AIStateMachine stateMachine;
    public NavMeshAgent agent;
    public GameObject target;

    private int attackRange;
    private Animator anim;

    //public float agentSpeed = 9.0f;

    public NavMeshHit lastHit;


    public OmnicatLabs.CharacterControllers.CharacterController characterController;

    //public void SetAgentSpeed(float speed)
    //{
        //agentSpeed = speed;
    //}

    public void Enter(AIStateMachine stateMachine) //First thing the state does.
    {
        anim = GetComponent<AIStateMachine>().anim;
        //Debug.Log("Entering Chase State");
        //agent.speed = agentSpeed;

        this.stateMachine = stateMachine;
        agent = GetComponentInParent<NavMeshAgent>();
        //target = GameObject.FindGameObjectWithTag("Player");

        //characterController = target.GetComponent<OmnicatLabs.CharacterControllers.CharacterController>();

        attackRange = gameObject.GetComponent<AIAttackState>().attackRange;

        anim.SetFloat("Sprint", 1);
    }

    public void Run() //Good ol update
    {

        if (ValidatePath(target))
        {
            agent.SetDestination(target.transform.position);
        }
        else if (Transitionable())
        {
            stateMachine.SetState(gameObject.GetComponent<AITransitionState>());
        }
        else if (Attackable(target) && characterController.isGrounded == true)
        {
            stateMachine.SetState(gameObject.GetComponent<AIAttackState>());
        }
      
        else if (characterController.isGrounded == true) //Prevent the AI from randomly patrolling while I am b hopping thru da club
        {
            stateMachine.SetState(gameObject.GetComponent<AIPatrolState>());
        }

       
    }

    public void Exit() //Last thing the state does before sending us wherever the user specified in update.
    {
        //Debug.Log("Exiting Chase State");
        anim.SetFloat("Sprint", 0);
    }

    public bool ValidatePath(GameObject _target)
    {

        if (target == null || _target == null)
        {
            target = GameObject.FindAnyObjectByType<OmnicatLabs.CharacterControllers.CharacterController>().gameObject;
            _target = GameObject.FindAnyObjectByType<OmnicatLabs.CharacterControllers.CharacterController>().gameObject;
        }
        
        if (agent.hasPath == true && agent.path.corners.Last() == _target.transform.position)
        {
            return true;
        }

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(_target.transform.position, path);

        //Debug.Log("Path: " + path.status);
        if (path.status == NavMeshPathStatus.PathComplete)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Attackable(GameObject _target)
    {
        if (NavMesh.SamplePosition(_target.transform.position, out lastHit, attackRange, NavMesh.AllAreas))
        {
            //Debug.Log("We hit: " + lastHit);
            if (Vector3.Distance(lastHit.position, _target.transform.position) <= attackRange)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else 
        { 
            return false; 
        }
    }

    public bool Transitionable()
    {
        NavMeshHit ai, player;

        NavMesh.SamplePosition(target.transform.position, out player, 10.0f, NavMesh.AllAreas);
        NavMesh.SamplePosition(agent.transform.position, out ai, 10.0f, NavMesh.AllAreas);


        if (ai.mask != player.mask)
        {
            Debug.Log("Transitionable");
            return true;
        }
        else
        {
            return false;
        }

    }
}
