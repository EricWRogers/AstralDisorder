using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableSaveController : MonoBehaviour, ISaveable
{
    private void Start()
    {
        GetComponent<Fracture>().callbackOptions.onFracture.AddListener((col, obj, vec) => SaveManager.Instance.Track(this));
    }

    public void OnReset()
    {
        gameObject.SetActive(true);
    }

    public void OnTrack()
    {
        
    }
}
