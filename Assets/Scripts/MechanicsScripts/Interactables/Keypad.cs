using UnityEngine;
using UnityEngine.UI;

public class Keypad : Interactable
{
    public GameObject keypadUI;
    public Canvas canvas;
    public KeypadDoor door;
    public RandNumGen codeGenerator;
    public UIStateMachineController uiController;

    public override void OnInteract()
    {
        base.OnInteract();

        var go = Instantiate(keypadUI, canvas.transform);
        go.GetComponent<KeypadUIController>().AdjustTimeScale();
        go.transform.SetAsLastSibling();

        if (door != null)
        {
            door.StartTracking();
            go.GetComponent<KeypadUIController>().correctPass = codeGenerator.RandNum;
            go.GetComponent<KeypadUIController>().controller = uiController;
            go.GetComponent<KeypadUIController>().onCorrectPassword.AddListener(door.Open);
            DefaultSelectionController.Instance.ChangeSelection(go.transform.GetChild(1).transform.GetChild(0).GetComponent<Button>());
        }

        uiController.ChangeState<HUDKeypadState>();

        //OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(true, OmnicatLabs.CharacterControllers.CharacterController.Instance.playerIsHidden, true);
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetLockedNoDisable(true, OmnicatLabs.CharacterControllers.CharacterController.Instance.playerIsHidden, true);
    }

    protected override void OnHover()
    {
        base.OnHover();

        GetComponent<Dialogue>().TriggerDialogue();
    }
}
