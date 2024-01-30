using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Keypad : Interactable
{
    public GameObject keypadUI;
    public Canvas canvas;
    public KeypadDoor door;
    public RandNumGen codeGenerator;
    public UIStateMachineController uiController;

    // Add a variable to represent difficulty level
    public enum DifficultyLevel { Easy, Normal, Hard, Extreme }
    public DifficultyLevel difficultyLevel;

    public override void OnInteract()
    {
        base.OnInteract();

        var go = Instantiate(keypadUI, canvas.transform);
        go.transform.SetAsLastSibling();

        if (door != null)
        {
            door.StartTracking();
            go.GetComponent<KeypadUIController>().correctPass = codeGenerator.RandNum;
            go.GetComponent<KeypadUIController>().controller = uiController;
            go.GetComponent<KeypadUIController>().onCorrectPassword.AddListener(door.Open);
        }

        uiController.ChangeState<HUDKeypadState>();

        // Depending on difficulty level, adjust time manipulation
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                FreezeTime();
                break;

            case DifficultyLevel.Normal:
                SlowDownTime();
                break;

            case DifficultyLevel.Hard:
                // No time change on harder difficulty
                break;
          
            case DifficultyLevel.Extreme:
                //no time change
                break;
        }

        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(true, OmnicatLabs.CharacterControllers.CharacterController.Instance.playerIsHidden, true);
    }

    // Add your time manipulation methods here
    private void FreezeTime()
    {
        Time.timeScale = 0;
    }

    private void SlowDownTime()
    {
        Time.timeScale = 0.4f;    }

    protected override void OnHover()
    {
        base.OnHover();

        GetComponent<Dialogue>().TriggerDialogue();
    }
}