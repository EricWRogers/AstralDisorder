using UnityEngine;
using UnityEngine.InputSystem;

public class HUDPauseState : UIState
{
    public CanvasGroup pauseGroup;
    private bool gameStarted = false;

    private void PostPlay()
    {
        gameStarted = true;
    }

    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(false, OmnicatLabs.CharacterControllers.CharacterController.Instance.playerIsHidden, true);
        pauseGroup.interactable = true;
        pauseGroup.blocksRaycasts = true;
        pauseGroup.alpha = 1f;
        Time.timeScale = 0f;
    }

    public override void OnStateUpdate(UIStateMachineController controller)
    {
        base.OnStateUpdate(controller);
    }

    public override void OnStateExit(UIStateMachineController controller)
    {
        base.OnStateExit(controller);
        pauseGroup.interactable = false;
        pauseGroup.blocksRaycasts = false;
        pauseGroup.alpha = 0f;
        Time.timeScale = 1f;
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && gameStarted && PauseMenu.GameIsPaused)
        {
            FindObjectOfType<PauseMenu>().GetComponent<PauseMenu>().Resume();
        }
    }
}
