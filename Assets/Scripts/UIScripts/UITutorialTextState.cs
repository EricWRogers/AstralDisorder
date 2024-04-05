using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

public class UITutorialTextState : UITextState
{
    [Tooltip("The exact name of the interaction in the controls file")]
    public string interactionName;
    protected int bindingIndex;
    //private void Start()
    //{
    //    Regex exp = new Regex(@"{([^}]+)}");

    //    string test = "Button";
    //    string result = exp.Replace(text, test);
    //    Debug.Log(result);
    //}

    protected virtual void DynamicTextUpdate(UIStateMachineController controller)
    {
        if (interactionName == "")
            return;

        Regex exp = new Regex(@"{([^}]+)}");

        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            bindingIndex = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions[interactionName].GetBindingIndex(group: "Controller");
        }
        else
        {
            bindingIndex = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions[interactionName].GetBindingIndex(group: "FPS MNK Controls");
        }

        string interactKeyName = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions[interactionName].GetBindingDisplayString(bindingIndex, out _, out _);
        controller.textArea.SetText(exp.Replace(text, interactKeyName));
    }

    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);

        controller.textArea.SetText(text);
    }

    public override void OnStateUpdate(UIStateMachineController controller)
    {
        base.OnStateUpdate(controller);

        DynamicTextUpdate(controller);
    }

    public override void OnStateExit(UIStateMachineController controller)
    {
        base.OnStateExit(controller);

        controller.textArea.SetText("");
    }
}
