using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSettingsMenu : UIState
{
    public CanvasGroup settingsGroup;
    public CanvasGroup displayGroup;

    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
        Time.timeScale = 0f;
        settingsGroup.alpha = 1f;
        settingsGroup.interactable = true;
        settingsGroup.blocksRaycasts = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void OnStateExit(UIStateMachineController controller)
    {
        Time.timeScale = 1f;
        settingsGroup.alpha = 0f;
        displayGroup.alpha = 0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        settingsGroup.interactable = false;
        settingsGroup.blocksRaycasts = false;
        displayGroup.interactable = false;
        displayGroup.blocksRaycasts = false;
        base.OnStateExit(controller);
    }
}
