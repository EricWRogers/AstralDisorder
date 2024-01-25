using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Audio;

public class RubbleSound : MonoBehaviour
{
    public void Sound()
    {
        AudioManager.Instance.Play("RubbleCrash");
    }

    public void PlayMonsterSound()
    {
        AudioManager.Instance.Play("MonsterCutsceneRoar");
    }
}
