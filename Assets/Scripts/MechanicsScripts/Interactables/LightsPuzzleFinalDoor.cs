using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Tween;

public class LightsPuzzleFinalDoor : MonoBehaviour
{
    public Transform doorPivot;
    public float timeToOpen = 2f;

    public void Open()
    {
        doorPivot.RealTweenYRot(90f, timeToOpen);
    }
}
