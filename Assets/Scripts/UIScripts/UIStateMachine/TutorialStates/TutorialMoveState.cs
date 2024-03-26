using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Timers;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

public class TutorialMoveState : UITutorialTextState
{
    public float lingerTime = 1f;
    public GameObject trigger;

    protected override void DynamicTextUpdate(UIStateMachineController controller)
    {
        Regex exp = new Regex(@"{([^}]+)}");

        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            text = text.Replace("Mouse", "Right Stick");
            controller.textArea.SetText(exp.Replace(text, "Left Stick"));
        }
        else
        {
            text = text.Replace("Right Stick", "Mouse");
            controller.textArea.SetText(exp.Replace(text, "WASD"));
        }
    }
}
