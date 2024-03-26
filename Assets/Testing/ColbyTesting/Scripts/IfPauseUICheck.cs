using UnityEngine;
using UnityEngine.UI;

public class IfPauseUICheck : MonoBehaviour
{
    private bool gameStarted = false;
    public Button playButton;
    public Button resumeButton;

    private void PostPlay()
    {
        gameStarted = true;
    }

    public void StartCheck()
    {
        if (gameStarted)
        {
            DefaultSelectionController.Instance.ChangeSelection(resumeButton);
        }
        else
        {
            DefaultSelectionController.Instance.ChangeSelection(playButton);
        }
    }
}