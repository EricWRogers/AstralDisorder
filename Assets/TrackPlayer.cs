using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = OmnicatLabs.CharacterControllers.CharacterController.Instance.transform;
    }

    void Update()
    {
        transform.LookAt(player.position);
    }
}
