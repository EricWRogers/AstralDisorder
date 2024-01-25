using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Tween;
using OmnicatLabs.Audio;

public class KeypadDoor : MonoBehaviour, ISaveable
{
    public Transform doorPivot;
    private Quaternion originalOrientation;
    public VirtualTrigger trigger;

    private void Start()
    {
        originalOrientation = doorPivot.rotation;
        if (trigger != null)
            trigger.triggerCallback.AddListener(HandleTrigger);
    }

    public void StartTracking()
    {
        SaveManager.Instance.Track(this);
    }

    public void OnReset()
    {
        doorPivot.rotation = originalOrientation;
    }

    public void OnTrack()
    {
        
    }

    private void HandleTrigger(VirtualTriggerContext ctx)
    {
        if (ctx.type == CallbackType.ENTER)
        {
            OmniTween.PauseTween(doorPivot);
        }
        if (ctx.type == CallbackType.EXIT)
        {
            OmniTween.Resume(doorPivot);
        }
    }

    public void Open()
    {
        doorPivot.RealTweenYRot(110f, 1.8f);
        AudioManager.Instance.Play("Door", gameObject);
    }
}
