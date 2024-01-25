using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Timers;

public class TutorialMoveState : UITextState
{
    public float lingerTime = 1f;
    public GameObject trigger;
    private bool moved;

    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
    }

    public override void OnStateUpdate(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
    }

    public override void OnStateExit(UIStateMachineController controller)
    {
        base.OnStateExit(controller);
    }
}
