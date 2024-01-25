using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Tween;

public class EscapePod : MonoBehaviour
{
    public Transform doorPivot;
    public Transform landing;
    public Transform curtainTop;
    public CameraShakeController shaker;
    public Animator credits;
    private bool unfurl = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorPivot.RealTweenZRot(0f, 1f, Launch);
        }

    }

    private void Launch()
    {
        shaker.CauseShake();
        shaker.onShakeFinish.AddListener(Unfurl);
        transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
        OmnicatLabs.CharacterControllers.CharacterController.Instance.transform.position = landing.position;
        
    }

    private void Unfurl()
    {
        unfurl = true;
        credits.SetTrigger("Win");
    }

    private void Update()
    {
        if (unfurl && curtainTop.localScale.y > .5f)
        {
            curtainTop.localScale = new Vector3(curtainTop.localScale.x, curtainTop.localScale.y * .8f, curtainTop.localScale.z);
        }
        
    }
}
