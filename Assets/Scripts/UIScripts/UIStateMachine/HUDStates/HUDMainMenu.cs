using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMainMenu : UIState
{
    public CanvasGroup group;
    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);

        group.alpha = 1f;
    }
}
