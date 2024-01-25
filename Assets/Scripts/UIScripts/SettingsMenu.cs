using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public UIStateMachineController controller;
    public Button backButton;
    public Button audioReturnButton;
    public Button displayOptionsButton;
    public CanvasGroup mainMenu;
    public CanvasGroup displaySettingsMenu;
    public CanvasGroup soundSettingsMenu;
    public CanvasGroup pauseMenu;

    private bool inMainMenu = true;

    private void Start()
    {
        backButton.onClick.AddListener(ExitToMainMenu);
        audioReturnButton.onClick.AddListener(OpenAudioSettingsMenu);
        displayOptionsButton.onClick.AddListener(OpenDisplayMenu);
    }

    private void PostPlay()
    {
        inMainMenu = false;
        backButton.onClick.RemoveListener(ExitToMainMenu);
        backButton.onClick.AddListener(Exit);

        //audioReturnButton.onClick.RemoveListener(OpenAudioSettingsMenu);
        //audioReturnButton.onClick.AddListener(OpenDisplayMenu);

        //displayOptionsButton.onClick.RemoveListener(OpenDisplayMenu);
        //displayOptionsButton.onClick.AddListener(OpenAudioSettingsMenu);
    }

    public void OpenDisplayMenu()
    {
        backButton.onClick.RemoveAllListeners();
        audioReturnButton.onClick.RemoveAllListeners();
        audioReturnButton.onClick.AddListener(OpenAudioSettingsMenu);
        //mainMenu.alpha = 0f;
        displaySettingsMenu.alpha = 1f;
        soundSettingsMenu.alpha = 0f;

        mainMenu.interactable = false;
        displaySettingsMenu.interactable = true;
        soundSettingsMenu.interactable = false;

        mainMenu.blocksRaycasts = false;
        displaySettingsMenu.blocksRaycasts = true;
        soundSettingsMenu.blocksRaycasts = false;

        GetComponentInChildren<Slider>().interactable = true;

        Debug.Log("Opening Graphics Display Menu");
    }

    public void OpenAudioSettingsMenu()
    {
        backButton.onClick.RemoveAllListeners();
        audioReturnButton.onClick.RemoveAllListeners();
        if (inMainMenu)
            backButton.onClick.AddListener(ExitToMainMenu);
        else
            backButton.onClick.AddListener(Exit);
        //mainMenu.alpha = 0f;
        displaySettingsMenu.alpha = 0f;
        soundSettingsMenu.alpha = 1f;

        mainMenu.interactable = false;
        displaySettingsMenu.interactable = false;
        soundSettingsMenu.interactable = true;

        mainMenu.blocksRaycasts = false;
        displaySettingsMenu.blocksRaycasts = false;
        soundSettingsMenu.blocksRaycasts = true;

        GetComponentInChildren<Slider>().interactable = true;

        Debug.Log("Opening Audio Settings Menu");
    }

        public void ExitToMainMenu()
    {
        mainMenu.alpha = 1f;
        displaySettingsMenu.alpha = 0f;
        soundSettingsMenu.alpha = 0f;
        soundSettingsMenu.interactable = false;
        soundSettingsMenu.blocksRaycasts = false;

        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true;

        displaySettingsMenu.interactable = false;
        soundSettingsMenu.interactable = false;

        //GetComponentInChildren<Slider>().interactable = false;

    }

    public void Exit()
    {
        mainMenu.alpha = 0f;
        displaySettingsMenu.alpha = 0f;
        soundSettingsMenu.alpha = 0f;

        mainMenu.interactable = false;
        displaySettingsMenu.interactable = false;
        soundSettingsMenu.interactable = false;

        mainMenu.blocksRaycasts = false;
        displaySettingsMenu.blocksRaycasts = false;
        soundSettingsMenu.blocksRaycasts = false;

        //controller.ChangeState(controller.nullState);
        pauseMenu.alpha = 1f;
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;

       // GetComponentInChildren<Slider>().interactable = false;

    }
    
}
