using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFlicker : MonoBehaviour
{
    public float minTime = 1.0f;
    public float maxTime = 5.0f;

    private void Start()
    {
        InvokeRepeating("Toggle", Random.Range(minTime, maxTime), Random.Range(minTime, maxTime));
    }

    public void Toggle()
    {
        var sys = GetComponent<ParticleSystem>();

        if (!sys.isPlaying)
        {
            sys.Play();
        }
        else
        {
            sys.Stop();
        }
    }
}
