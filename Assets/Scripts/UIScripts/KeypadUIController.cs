using UnityEngine;
using TMPro;
using UnityEngine.Events;
using OmnicatLabs.Timers;
using OmnicatLabs.Audio;

public class KeypadUIController : MonoBehaviour
{
    public TMP_Text displayText;
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;
    private float timeAfterSubmit = .5f;
    private string input;
    private float buttonCount = 0;
    private float guesses;
    [HideInInspector]
    public string correctPass;
    public GameObject keypadUI;
    public UIStateMachineController controller;

    private KeyCode[] validKeys = {
        KeyCode.Keypad0, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4,
        KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9,
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
        KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };

    private float previousTimeScale = 1f;

    private void Start()
    {
        guesses = correctPass.Length;
        Debug.Log(correctPass);
    }

    public void ValueEntered(string valueEntered)
    {
        switch (valueEntered)
        {
            case "Q":
                Quit();
                break;
            case "C":
                Clear();
                break;
            default:
                if (buttonCount < 4)
                {
                    buttonCount++;
                    input += valueEntered;
                    displayText.text = input.ToString();
                    AudioManager.Instance.Play("Keypress");
                }
                if (buttonCount == 4)
                {
                    Submit();
                }
                break;
        }
    }

    public void Quit()
    {
        controller.ChangeState(controller.nullState);

        // Reset time scale to previous value
        Time.timeScale = 1f;
    }

    public void Clear()
    {
        input = "";
        buttonCount = 0;
        displayText.text = input.ToString();
    }

    public void Submit()
    {
        if (input != null)
        {
            if (input == correctPass)
            {
                displayText.text = "<color=#15F00B>" + input.ToString();
                onCorrectPassword.Invoke();
                TimerManager.Instance.CreateTimer(timeAfterSubmit, () => { Quit(); });

                // Adjust game time based on difficulty
              
                previousTimeScale = Time.timeScale;
                Time.timeScale =1f ;

                AudioManager.Instance.Play("Keypadsuccess");
            }
            else
            {
                displayText.text = "<color=#F00B0B>" + input.ToString();
                onIncorrectPassword.Invoke();
                TimerManager.Instance.CreateTimer(timeAfterSubmit, () => { Clear(); });
                AudioManager.Instance.Play("Keypadfail");
            }
        }
    }

    public void ClearInput()
    {
        input = "";
        displayText.text = input.ToString();
    }

    public void OpenMenu()
    {
        // Store previous time scale
        previousTimeScale = Time.timeScale;

        // Adjust time scale based on difficulty
       
        Difficulty difficulty = GameManager.Instance.GetDifficulty();
        switch (difficulty)
        {
            case Difficulty.Easy:
                Time.timeScale = 0f; // Pause time
                break;
            case Difficulty.Normal:
                Time.timeScale = 0.5f; // Half time
                break;
            case Difficulty.Hard:
                Time.timeScale = 1f;
                break;
            case Difficulty.Extreme:
                Time.timeScale = 1f; // Normal time
                break;
        }
    

        // Activate keypad UI
        keypadUI.SetActive(true);
    }

    public void CloseMenu()
    {
        // Reset time scale to previous value
        Time.timeScale = 1f;

        // Deactivate keypad UI
        keypadUI.SetActive(false);
    }

    private void Update()
    {
        foreach (KeyCode keyCode in validKeys)
        {
            if (Input.GetKeyDown(keyCode))
            {
                HandleInput(keyCode);
            }
        }
    }

    private void HandleInput(KeyCode keyCode)
    {
        string inputFromKeyCode = KeyCodeToStringCheck(keyCode);

        if (inputFromKeyCode == "Backspace")
        {
            ClearInput();
        }

        else if (!string.IsNullOrEmpty(inputFromKeyCode))
        {
            if (buttonCount < 4)
            {
                buttonCount++;
                input += inputFromKeyCode;
                displayText.text = input.ToString();

                if (buttonCount == 4)
                {
                    Submit();
                }
            }
        }
    }

    private string KeyCodeToStringCheck(KeyCode keyCode)
    {
        if (keyCode == KeyCode.Backspace)
        {
            return "Backspace";
        }

        else if (keyCode >= KeyCode.Keypad0 && keyCode <= KeyCode.Keypad9)
        {
            return (keyCode - KeyCode.Keypad0).ToString();
        }

        else if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
        {
            return (keyCode - KeyCode.Alpha0).ToString();
        }

        return string.Empty;
    }
}
