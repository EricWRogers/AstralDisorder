using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

public class TutorialLookState : UITutorialTextState
{
    protected override void DynamicTextUpdate(UIStateMachineController controller)
    {
        Regex exp = new Regex(@"{([^}]+)}");

        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            controller.textArea.SetText(exp.Replace(text, "right stick"));
        }
        else
        {
            controller.textArea.SetText(exp.Replace(text, "mouse"));
        }   
    }
}
