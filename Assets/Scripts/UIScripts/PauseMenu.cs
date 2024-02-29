using OmnicatLabs.CharacterControllers;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public float fadeTime = .3f;
    public CanvasGroup pauseMenuUI;
    public Slider sensitivitySlider;
    public UIStateMachineController controller;
    public MouseLook mouseLook;

    public void Resume()
    {
        Time.timeScale = 1.0f;
        controller.ChangeState<HUDIdleState>();
        GameIsPaused = false;
        mouseLook.enabled = true;
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(false, false, false);
        pauseMenuUI.alpha = 0f;
        pauseMenuUI.blocksRaycasts = false;
        pauseMenuUI.interactable = false;
    }

    public void Settings()
    {
        controller.ChangeState<HUDSettingsMenu>();
        pauseMenuUI.alpha = 0f;
        pauseMenuUI.blocksRaycasts = false;
        pauseMenuUI.interactable = false;

        sensitivitySlider.interactable = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}