using UnityEngine;
using OmnicatLabs.Tween;
using System.Reflection;
using System.Linq;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public UIStateMachineController controller;
    public OpeningCutscene cutscene;
    public CanvasGroup settingsMenu;
    public float menuCloseTime = 2f;
    public GameObject crosshair;

    private void Awake()
    {
        //this is needed to not cause issues with sound and other parts of the game. In general I can't think of a reason you would not want this.
        Application.runInBackground = true;
    }

    private void Start()
    {
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(true, true, true);
        crosshair.SetActive(false);
    }

    public void Play()
    {
        GetComponent<CanvasGroup>().FadeOut(menuCloseTime, StartGame, EasingFunctions.Ease.EaseOutQuart);
        controller.ChangeState(controller.nullState);
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void StartGame()
    {
        GetComponent<Dialogue>().TriggerDialogue();
        crosshair.SetActive(true);
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(false, true, false);
        cutscene.StartCutscene();
        InvokePostPlay();
    }

    //this is what calls all the PostPlay functions in every script
    public void InvokePostPlay()
    {
        var assemblyTypes = Assembly.GetAssembly(GetType()).GetTypes().Where(type => !type.IsGenericType && typeof(MonoBehaviour).IsAssignableFrom(type));

        foreach (var type in assemblyTypes)
        {
            var instances = FindObjectsOfType(type);
            foreach (var instance in instances)
            {
                var method = type.GetMethod("PostPlay", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(instance, null);
            }
        }
    }

    public void Settings()
    {
        settingsMenu.alpha = 1f;
        settingsMenu.interactable = true;
        settingsMenu.blocksRaycasts = true;
        settingsMenu.GetComponentInChildren<Slider>().interactable = true;
        GetComponent<CanvasGroup>().interactable = false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
