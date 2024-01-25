using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDIdleState : UINullState
{
    private bool gameStarted = false;
    private void PostPlay()
    {
        gameStarted = true;
    }
    public override void OnStateUpdate(UIStateMachineController controller)
    {
        base.OnStateUpdate(controller);

        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            controller.ChangeState<HUDPauseState>();
        }
    }
}
