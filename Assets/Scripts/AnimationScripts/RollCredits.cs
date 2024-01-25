using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollCredits : MonoBehaviour
{
    public CameraShakeController shaker;
    OmnicatLabs.CharacterControllers.CharacterController player;

    private void Start()
    {
        player = OmnicatLabs.CharacterControllers.CharacterController.Instance;
    }

    public void Run()
    {
        shaker.CauseShake();
        shaker.onShakeFinish.AddListener(AfterShake);
        transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 50f, transform.parent.position.z);
        player.transform.position = transform.parent.position;
    }

    private void AfterShake()
    {

    }

}
