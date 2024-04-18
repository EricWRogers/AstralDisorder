using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Tween;
using OmnicatLabs.Audio;

public class EscapePod : MonoBehaviour
{
    public Transform doorPivot;
    public Transform landing;
    public Transform curtainTop;
    public CameraShakeController shaker;
    public Animator credits;
    private bool unfurl = false;

    private void Start()
    {
        shaker = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponentInChildren<CameraShakeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorPivot.TweenLocalZRotation(0f, 1f, Launch);
        }

    }

    private void Launch()
    {
        AudioManager.Instance.Stop("SpaceAmbience");
        AudioManager.Instance.Stop("MechanicalAmbience");
        shaker.CauseShake();
        AudioManager.Instance.Play("EscapePodLaunch");
        shaker.onShakeFinish.AddListener(Unfurl);
        transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
        OmnicatLabs.CharacterControllers.CharacterController.Instance.transform.position = landing.position;
        
    }

    private void Unfurl()
    {
        unfurl = true;
        AudioManager.Instance.Stop("EscapePodLaunch");
        credits.SetTrigger("Win");
        AudioManager.Instance.Play("CreditsBGM");
    }

    private void Update()
    {
        if (unfurl && curtainTop.localScale.y > .5f)
        {
            curtainTop.localScale = new Vector3(curtainTop.localScale.x, curtainTop.localScale.y * .8f, curtainTop.localScale.z);
        }
        
    }
}
