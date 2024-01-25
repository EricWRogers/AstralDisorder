using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDKeypadState : UIState
{
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

        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(false, OmnicatLabs.CharacterControllers.CharacterController.Instance.playerIsHidden, false);
        Destroy(FindObjectOfType<KeypadUIController>().gameObject);
    }
}
