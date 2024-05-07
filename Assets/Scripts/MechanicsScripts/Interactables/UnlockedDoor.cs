using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Tween;
using OmnicatLabs.Audio;

public class UnlockedDoor : Interactable
{
    public Transform doorPivot;
    private Quaternion originalOrientation;
    public VirtualTrigger trigger;

    protected override void Start()
    {
        originalOrientation = doorPivot.rotation;
        if (trigger != null)
            trigger.triggerCallback.AddListener(HandleTrigger);
    }

    public override void OnReset()
    {
        doorPivot.rotation = originalOrientation;
    }

    public override void OnTrack()
    {

    }

    public override void OnInteract()
    {
        base.OnInteract();
        Open();
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
        doorPivot.TweenLocalYRotation(110f, 1.8f);
        AudioManager.Instance.Play("Door", gameObject);
        GetComponent<Dialogue>().TriggerDialogue();
        canInteract = false;
    }
}
