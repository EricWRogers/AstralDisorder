using UnityEngine;

public class HUDPauseState : UIState
{
    public CanvasGroup pauseGroup;
    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
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
}
