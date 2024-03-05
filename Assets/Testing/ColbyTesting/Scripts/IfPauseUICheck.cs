using UnityEngine;
using UnityEngine.UI;

public class IfPauseUICheck : MonoBehaviour
{
    public bool gameStarted = false;
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
            resumeButton.Select();
        }
        else
        {
            playButton.Select();
        }
    }
}