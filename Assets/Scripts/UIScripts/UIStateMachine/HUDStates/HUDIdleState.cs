using OmnicatLabs.CharacterControllers;
using UnityEngine.InputSystem;

public class HUDIdleState : UINullState
{
    private bool gameStarted = false;
    private UIStateMachineController inputController;
    public MouseLook mouseLook;
    private void PostPlay()
    {
        gameStarted = true;
    }

    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
        inputController = controller;
    }

    public override void OnStateUpdate(UIStateMachineController controller)
    {
        base.OnStateUpdate(controller);

        /*
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            controller.ChangeState<HUDPauseState>();
        }
        */
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && gameStarted && !PauseMenu.GameIsPaused)
        {
            PauseMenu.GameIsPaused = true;
            mouseLook.enabled = false;
            inputController.ChangeState<HUDPauseState>();
        }
    }
}
