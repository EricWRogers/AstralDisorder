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

    private void Start()
    {
        //correctPass = "123";
        //correctPass = FindObjectOfType<RandNumGen>().RandNum.ToString();
        guesses = correctPass.Length;
        Debug.Log(correctPass);
    }

    public void ValueEntered(string valueEntered)
    {
        switch (valueEntered)
        {
            case "Q":
                //Debug.Log("Quit");
                Quit();
                break;
            case "C":
                //Debug.Log("Clear");
                Clear();
                break;
            default:
                //Debug.Log("Default");
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

    //void Update()
    //{
    //    if (buttonCount == guesses)
    //    {
    //        if (input == correctPass)
    //        {
    //            displayText.text = "<color=#15F00B>" + input.ToString();
    //            buttonCount = 0;
    //            Debug.Log("Correct");
    //            onCorrectPassword.Invoke();
    //        }
    //        else
    //        {
    //            displayText.text = "<color=#F00B0B>" + input.ToString();
    //            //wrongTimer.StartTimer(wrongTimer.countDownTime, wrongTimer.autoRestart);
    //            buttonCount = 0;
    //        }
    //    }
    //}

    public void ClearInput()
    {
        input = "";
        displayText.text = input.ToString();
    }

    public void CloseMenu()
    {
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