using UnityEngine;
using TMPro;
using OmnicatLabs.Timers;
using OmnicatLabs.Tween;

public class ObjectiveText : MonoBehaviour
{
    public TextMeshProUGUI objText;
    public CanvasGroup objArea;
    public float fadeTime = 1f;
    [TextArea]
    public string startingObjective;
    public string currObjective;
    public int currObjectivePriority = 0;
    
    private Timer currentTimer;

    public static ObjectiveText instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void PostPlay()
    {
        FirstCurrObjective();
    }

    public void FirstCurrObjective()
    {                 
        objArea.FadeIn(fadeTime);
        objText.SetText(startingObjective);
    }

    public void SetCurrObjective(string newObjective)
    {
        if (currentTimer != null)
        {
            TimerManager.Instance.Stop(currentTimer);
        }

        currObjective = newObjective;

        objArea.FadeOut(fadeTime,
           () => {
               TimerManager.Instance.CreateTimer(0.1f,
               () => {
                   objText.SetText(currObjective);
                   objArea.FadeIn(fadeTime);
               }, out currentTimer);
           }
        );
    }

    public void SetCurrObjectivePriority(int newPriority)
    {
        currObjectivePriority = newPriority;
    }
}
