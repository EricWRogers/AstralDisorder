using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightPuzzleLights : MonoBehaviour, ISaveable
{
    public bool lit = false;
    public Material litMat;

    public UnityEvent lightStateChange = new UnityEvent();

    private Material offMat;
    private MeshRenderer mesh;
    private bool tracked = false;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        offMat = mesh.material;
    }

    //Changing the light state and sending a debug message
    public void ChangeLightState()
    {
        if (!tracked)
        {
            SaveManager.Instance.Track(this);
        }

        lit = !lit;

        if (lit)
        {
            Debug.Log("Change happened");
            mesh.material = litMat;
        }
        else
        {
            mesh.material = offMat;
        }

        lightStateChange.Invoke();

        LightPuzzle.Instance.CheckPuzzleWin();

        //string boolTest;
        //if (lit)
        //    boolTest = "lit";
        //else
        //    boolTest = "not lit";
         
        
        //Debug.Log(this.gameObject.name + " is " + boolTest);
    }

    public void OnTrack()
    {
        
    }

    public void OnReset()
    {
        if (!LightPuzzle.isCompleted)
        {
            tracked = false;
            mesh.material = offMat;
            lit = false;
        }
    }
}
