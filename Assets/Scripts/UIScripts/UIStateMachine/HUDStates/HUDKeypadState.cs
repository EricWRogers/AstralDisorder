using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDKeypadState : UIState
{
    private bool playerHiddenState;

    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
        playerHiddenState = OmnicatLabs.CharacterControllers.CharacterController.Instance.playerIsHidden;
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetLockedNoDisable(true, true, true);
    }

    public override void OnStateUpdate(UIStateMachineController controller)
    {
        base.OnStateUpdate(controller);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controller.ChangeState(controller.nullState);
        }
    }

    public override void OnStateExit(UIStateMachineController controller)
    {
        base.OnStateExit(controller);

        //OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(false, OmnicatLabs.CharacterControllers.CharacterController.Instance.playerIsHidden, false);
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetLockedNoDisable(false, playerHiddenState, false);
        Destroy(FindObjectOfType<KeypadUIController>().gameObject);
        Time.timeScale = 1.0f;
    }
}
