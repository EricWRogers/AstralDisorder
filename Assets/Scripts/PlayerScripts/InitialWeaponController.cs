using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialWeaponController : MonoBehaviour
{
    private OmnicatLabs.CharacterControllers.CharacterController controller;

    void Start()
    {
        controller = OmnicatLabs.CharacterControllers.CharacterController.Instance;

        //controller.SetControllerLocked(false, true, false);
    }

    public void OnWeaponPickup()
    {
        controller.SetControllerLocked(false, false, false);
    }
}
