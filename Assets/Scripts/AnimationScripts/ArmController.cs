using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    private string currentState;
    [HideInInspector]
    public Animator anim;
    public GameObject grappleModel;

    public static ArmController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EnableGrapple()
    {
        grappleModel.SetActive(true);
    }

    public void DisableGrapple()
    {
        grappleModel.SetActive(false);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;
        anim.CrossFadeInFixedTime(currentState, 0.2f);
        
    }
}
