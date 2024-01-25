using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Audio;

public class DoorSoundPlayer : MonoBehaviour
{
    public void Play()
    {
        AudioManager.Instance.Play("Door", transform.position);
    }
}
