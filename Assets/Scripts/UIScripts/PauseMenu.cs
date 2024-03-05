using OmnicatLabs.CharacterControllers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public float fadeTime = .3f;
    public CanvasGroup pauseMenuUI;
    public Slider sensitivitySlider;
    public UIStateMachineController controller;
    public MouseLook mouseLook;
    public Button defaultButton;

    public static PauseMenu Instance;
    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void PostPlay()
    {
        gameStarted = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        controller.ChangeState<HUDIdleState>();
        mouseLook.Unlock();
        OmnicatLabs.CharacterControllers.CharacterController.Instance.TogglePause();
        pauseMenuUI.alpha = 0f;
        pauseMenuUI.blocksRaycasts = false;
        pauseMenuUI.interactable = false;
        GameIsPaused = false;
    }

    public void Settings()
    {
        controller.ChangeState<HUDSettingsMenu>();
        pauseMenuUI.alpha = 0f;
        pauseMenuUI.blocksRaycasts = false;
        pauseMenuUI.interactable = false;

        sensitivitySlider.interactable = true;
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && gameStarted)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                GameIsPaused = true;
                mouseLook.Lock();
                OmnicatLabs.CharacterControllers.CharacterController.Instance.TogglePause();
                controller.ChangeState<HUDPauseState>();
                defaultButton.Select();
            }
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}