using UnityEngine;
using UnityEngine.Events;
using OmnicatLabs.Tween;
using OmnicatLabs.Audio;

public class MotionScanner : MonoBehaviour
{
    GameObject player;
    public Transform doorPivot;
    public VirtualTrigger doorTrigger;
    public float openAngle = -50f;
    public float timeToOpen = 5f;
    public UnityEvent onDoorClose = new UnityEvent();
    public UnityEvent onDoorOpen = new UnityEvent();
    public AirlockDoorButton buttonForThisDoor;
    public AirlockDoorButton buttonForOtherDoor;
    public bool doorOpening = false;
    [HideInInspector]
    public bool doorOpened = false;
    public bool doorClosed = true;

    private void Start()
    {
        onDoorClose.AddListener(OnDoorClose);
        onDoorOpen.AddListener(OnDoorOpen);
        doorTrigger.triggerCallback.AddListener(CheckPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!doorOpened)
                AudioManager.Instance.Play("Door", gameObject);

            OmniTween.CancelTween(doorPivot);
            doorPivot.RealTweenYRot(openAngle, timeToOpen, () => onDoorOpen.Invoke());
            doorOpened = true;
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
            doorPivot.RealTweenYRot(0f, timeToOpen, () => onDoorClose.Invoke());
        }
    }

    private void OnDoorClose()
    {
        doorClosed = true;
        buttonForThisDoor.SetInteractable(true);
        buttonForOtherDoor.SetInteractable(true);
    }

    private void OnDoorOpen()
    {
        buttonForOtherDoor.SetInteractable(false);
        buttonForThisDoor.SetInteractable(false);
    }

    //private void OnTriggerEnter(Collider collider)
    //{
    //    //If the player is within the collider, open the door
    //    if (collider.CompareTag("Player") && !doorOpening)
    //    {
    //        //OmniTween.CancelTween(doorPivot);
    //        doorPivot.TweenYRot(openAngle, timeToOpen, () => onDoorOpen.Invoke());
    //        doorOpening = true;
    //        doorClosed = false;
    //        buttonForOtherDoor.SetInteractable(false);
    //        buttonForThisDoor.SetInteractable(false);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Exit");
    //    //OmniTween.CancelTween(doorPivot);
    //    doorPivot.TweenYRot(-openAngle, timeToOpen, () => onDoorClose.Invoke());
    //}

    //void DoorOpen()
    //{
    //    doorPivot.TweenYRot(-openAngle, 2f, () => onDoorClose.Invoke());
    //}

    //void DoorClose()
    //{
    //    doorClosed = true;
    //    doorOpening = false;
    //    buttonForOtherDoor.SetInteractable(true);
    //    buttonForThisDoor.SetInteractable(true);
    //}
}
