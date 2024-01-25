using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Tween;
using OmnicatLabs.Audio;

public class LightPuzzleLever : Interactable
{
    public Transform leverPivot;
    public float flipTime = .7f;
    public Material wireOnMaterial;
    //Set lights to toggle in inspector
    public List<GameObject> linkedLights = new List<GameObject>();
    public List<GameObject> wires = new List<GameObject>();

    private bool isFlipped = false;
    private Material wireOffMaterial;


    protected override void Start()
    {
        base.Start();
        if (wires.Count > 0)
            wireOffMaterial = wires[0].GetComponent<MeshRenderer>().material;
    }

    public override void OnReset()
    {
        if (!LightPuzzle.isCompleted)
        {
            base.OnReset();
            isFlipped = false;
            leverPivot.localRotation = Quaternion.identity;
            wires.ForEach(wire => wire.GetComponent<MeshRenderer>().material = wireOffMaterial);
        }
    }

    //changes state of each light
    public override void OnInteract()
    {
        base.OnInteract();

        AudioManager.Instance.Play("Lever");

        isFlipped = !isFlipped;

        if (!isFlipped)
        {
            leverPivot.RealTweenZRot(0, flipTime);
        }
        else
        {
            //Need to rework part of the audio manager for this to work AudioManager.Instance.Play("PowerOn", gameObject);
            leverPivot.RealTweenZRot(-85f, flipTime);
        }

        for(int i = 0; i < linkedLights.Count; i++)
        {
            linkedLights[i].GetComponentInChildren<LightPuzzleLights>().ChangeLightState();
        }

        foreach (var wire in wires)
        {
            if (isFlipped)
            {
                wire.GetComponent<MeshRenderer>().material = wireOnMaterial;
            }
            else
            {
                wire.GetComponent<MeshRenderer>().material = wireOffMaterial;
            }
        }
    }

}
