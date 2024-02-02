using UnityEngine;
using TMPro;
using OmnicatLabs.Timers;
using OmnicatLabs.Tween;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public CanvasGroup dialogueArea;
    public float dialogueFadeTime = .3f;

    private TextMeshProUGUI textArea;
    private Timer currentTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (dialogueArea != null)
            textArea = dialogueArea.GetComponentInChildren<TextMeshProUGUI>();
        else Debug.Log("Could not find dialogueArea. Make sure it is assigned in the inspector");
    }

    public void ShowDialogue(string message)
    {
        textArea.SetText(message);

        dialogueArea.FadeIn(dialogueFadeTime);

        if (currentTimer != null)
        {
            TimerManager.Instance.Stop(currentTimer);
        }
    }

    public void ShowDialogue(string message, float timeOnScreen)
    {
        if (currentTimer != null)
        {
            TimerManager.Instance.Stop(currentTimer);

            //textArea.SetText(message);

            //TimerManager.Instance.CreateTimer(timeOnScreen,
            //    () =>
            //    {
            //        dialogueArea.FadeOut(dialogueFadeTime);
            //    },
            //    out currentTimer);
            ////return;
        }

        textArea.SetText(message);

        dialogueArea.FadeIn(dialogueFadeTime, 
            () => { TimerManager.Instance.CreateTimer(timeOnScreen,
                () => {
                    dialogueArea.FadeOut(dialogueFadeTime);
                }, out currentTimer); 
            });
    }

    public void ShowDialogue(string message, float timeOnScreen, UnityAction onEnd)
    {
        if (currentTimer != null)
        {
            TimerManager.Instance.Stop(currentTimer);

            textArea.SetText(message);

            TimerManager.Instance.CreateTimer(timeOnScreen,
                () =>
                {
                    dialogueArea.FadeOut(dialogueFadeTime, () => onEnd.Invoke());
                },
                out currentTimer);
            return;
        }

        textArea.SetText(message);

        dialogueArea.FadeIn(dialogueFadeTime,
            () => {
                TimerManager.Instance.CreateTimer(timeOnScreen,
            () => {
                dialogueArea.FadeOut(dialogueFadeTime, () => onEnd.Invoke());
            }, out currentTimer);
            });
    }

    public void ClearDialogue()
    {
        textArea.SetText("");
        dialogueArea.FadeOut(dialogueFadeTime);
    }
}
