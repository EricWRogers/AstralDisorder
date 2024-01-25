using UnityEngine;
using OmnicatLabs.Tween;
using UnityEngine.Events;
using OmnicatLabs.Timers;
using OmnicatLabs.Audio;

public class AirlockDoorButton : Interactable
{
    public Transform doorPivot;
    public bool startInteractable = true;
    public float toAngle = -50f;
    public float timeToOpen = 25f;
    public UnityEvent onDoorClose = new UnityEvent();
    public UnityEvent onDoorOpen = new UnityEvent();
    public AirlockDoorButton buttonForOtherDoor;
    public MotionScanner doorToOpen;
    public MotionScanner doorBehind;

    protected override void Start()
    {
        base.Start();

        if (!startInteractable)
        {
            SetInteractable(false);
        }

        //onDoorOpen.AddListener(DoorOpen);
        //onDoorClose.AddListener(DoorClose);
    }

    public override void OnInteract()
    {
        base.OnInteract();

        if (doorBehind.doorClosed)
        {
            OmniTween.CancelTween(doorPivot, true);
            AudioManager.Instance.Play("Door", gameObject);
            doorPivot.RealTweenYRot(toAngle, timeToOpen, () => onDoorOpen.Invoke());
            doorToOpen.doorOpened = true;
            SetInteractable(false);
            buttonForOtherDoor.SetInteractable(false);
        }
    }

    //public override void OnInteract()
    //{
    //    base.OnInteract();

    //    if (doorToOpen.doorClosed && doorBehind.doorClosed)
    //    {
    //        doorPivot.TweenYRot(directionalForce, 2f, () => onDoorOpen.Invoke());
    //        doorToOpen.doorOpening = true;
    //        doorToOpen.doorClosed = false;
    //        SetInteractable(false);
    //        buttonForOtherDoor.SetInteractable(false);
    //    }
    //}

    //void DoorOpen()
    //{
    //    doorPivot.TweenYRot(-directionalForce, 2f, () => onDoorClose.Invoke());
    //}

    //void DoorClose()
    //{
    //    doorToOpen.doorClosed = true;
    //    doorToOpen.doorOpening = false;
    //    SetInteractable(true);
    //    buttonForOtherDoor.SetInteractable(true);
    //}
}
