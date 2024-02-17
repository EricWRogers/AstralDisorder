using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    private Spring spring;
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void DrawRope()
    {

    }
}
