using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLoseController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameStateController.Instance.ActivateLose();
        }
    }
}
