using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTutorialTrigger : MonoBehaviour
{
    public UIStateMachineController controller;
    public Fracture wall;
    private bool hasDestroyed = false;

    private void Start()
    {
        wall.callbackOptions.onFracture.AddListener((col, obj, vec) => {
            hasDestroyed = true;
            controller.ChangeState<TutorialCrouchState>();
            if (TryGetComponent(out ChangeObjective changeObjective))
            {
                changeObjective.Change();
            }
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasDestroyed)
            {
                controller.ChangeState<TutorialCrouchState>();
            }
            else
            {
                controller.ChangeState<TutorialDestructionState>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.ChangeState(controller.nullState);
        }
    }
}
