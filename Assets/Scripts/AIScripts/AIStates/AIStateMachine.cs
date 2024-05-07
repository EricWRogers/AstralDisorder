using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyState //Make the states inherit from this. Basically will force that script to have these functions. If it dont it dont work.
{
    void Enter(AIStateMachine stateMachine);
    void Run();
    void Exit();
}

public class AIStateMachine : MonoBehaviour //Dont touch this script.
{
    public string state;
    public Animator anim;
    //[HideInInspector]
    [SerializeField]
    public IEnemyState currentState; //DONT TOUCH 

    private NavMeshAgent agent;



    private void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        SetState(gameObject.GetComponent<AIIdleState>());
    }

    private void Update()
    {
        
        state = currentState.ToString();
        Debug.Log($"Monster State: {currentState}");
        //stateVisualizer.text = currentState.ToString(); //Testing

        if (!agent.isStopped)
        {
            currentState.Run();
        }
        else
        {
            currentState.Exit();
        }

        if(currentState == null) 
        {
            Debug.Log("Current State Null");
            SetState(gameObject.GetComponent<AIIdleState>());
        }
        
    }

    public void SetState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }


   public void AIStop(bool _value)
    {
        agent.isStopped = _value;
        Debug.Log("Agent isStopped = " + _value);
    }


}
