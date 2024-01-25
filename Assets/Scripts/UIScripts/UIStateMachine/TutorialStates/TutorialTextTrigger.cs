using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Timers;

public class TutorialTextTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        Move,
        Look,
        Jump,
        Crouch,
        Sprint,
        Interact,
        Destruction,
    }

    public UIStateMachineController tutorialController;
    public TriggerType triggerType;
    public float lingerTime = 1.2f;
    public bool oneTime = false;
    public Dialogue exitDialogue;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (triggerType)
            {
                case TriggerType.Move:
                    tutorialController.ChangeState<TutorialMoveState>();
                    break;
                case TriggerType.Look:
                    tutorialController.ChangeState<TutorialLookState>();
                    break;
                case TriggerType.Jump:
                    tutorialController.ChangeState<TutorialJumpState>();
                    break;
                case TriggerType.Crouch:
                    tutorialController.ChangeState<TutorialCrouchState>();
                    break;
                case TriggerType.Sprint:
                    tutorialController.ChangeState<TutorialSprintState>();
                    break;
                case TriggerType.Interact:
                    tutorialController.ChangeState<TutorialInteractState>();
                    break;
                case TriggerType.Destruction:
                    tutorialController.ChangeState<TutorialDestructionState>();
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimerManager.Instance.CreateTimer(lingerTime, () => tutorialController.ChangeState(tutorialController.nullState));
            if (oneTime)
            {
                gameObject.SetActive(false);
            }

            if (exitDialogue != null)
            {
                exitDialogue.TriggerDialogue();
            }
        }
    }
}
