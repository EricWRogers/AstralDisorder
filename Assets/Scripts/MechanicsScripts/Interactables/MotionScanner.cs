using UnityEngine;
using UnityEngine.Events;
using OmnicatLabs.Tween;
using OmnicatLabs.Audio;
using OmnicatLabs.Timers;
using OmnicatLabs.Events;
using System.Collections.Generic;
using System.Linq;

public class MotionScanner : MonoBehaviour
{
    public Transform doorPivot;
    public VirtualTrigger doorTrigger;
    public float openAngle = -50f;
    public float timeToOpen = 5f;
    public Checkpoint airLockZone;
    public MotionScanner otherDoor;
    public UnityEvent onDoorClose = new UnityEvent();
    public UnityEvent onDoorOpen = new UnityEvent();
    [StringDropdown(EditorStringLists.BuildScenes)]
    public int sceneToLoad;
    [StringDropdown(EditorStringLists.BuildScenes)]
    public int sceneToUnload;
    public float minWaitTime = 1.5f;
    public bool doorOpening = false;
    [HideInInspector]
    public bool doorOpened = false;
    public bool doorClosed = true;
    public static MotionScanner lastDoorOpened;
    private UnityEvent onLoadComplete;
    private List<ParticleSystem> effects = new List<ParticleSystem>();

    private void Start()
    {
        onDoorClose.AddListener(OnDoorClose);
        onDoorOpen.AddListener(OnDoorOpen);
        doorTrigger.triggerCallback.AddListener(CheckPlayer);
        effects = airLockZone.smokeParent.GetComponentsInChildren<ParticleSystem>().ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open();
        }
    }

    public void CheckPlayer(VirtualTriggerContext ctx)
    {
        if (ctx.type == CallbackType.ENTER)
            OmniTween.PauseTween(doorPivot);
        if (ctx.type == CallbackType.EXIT)
            OmniTween.Resume(doorPivot);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OmniTween.CancelTween(doorPivot, true);
            doorOpened = false;
            doorPivot.TweenLocalYRotation(0f, timeToOpen, () => onDoorClose.Invoke());
        }
    }

    public void Open()
    {
        if (!doorOpened)
            AudioManager.Instance.Play("Door", gameObject);

        OmniTween.CancelTween(doorPivot);
        doorPivot.TweenLocalYRotation(openAngle, timeToOpen, () => onDoorOpen.Invoke());
        doorOpened = true;
        doorClosed = false;
    }

    private void OnDoorClose()
    {
        doorClosed = true;
        if (airLockZone.playerIsHere && otherDoor.doorClosed)
        {
            onLoadComplete = LoadManager.ChangeScenes(sceneToLoad, sceneToUnload);
            onLoadComplete.AddListener(OnLoadDone);
            effects.ForEach(effect => effect.Play());
            AudioManager.Instance.Play("SteamRelease");
        }
    }

    private void OnLoadDone()
    {
        TimerManager.Instance.CreateTimer(minWaitTime, OnWaitDone);
        onLoadComplete.RemoveListener(OnLoadDone);
    }

    private void OnWaitDone()
    {
        otherDoor.Open();
        lastDoorOpened = otherDoor;
        effects.ForEach(effect => effect.Stop());
        AudioManager.Instance.Stop("SteamRelease");
    }

    private void OnDoorOpen()
    {
        
    }
}
