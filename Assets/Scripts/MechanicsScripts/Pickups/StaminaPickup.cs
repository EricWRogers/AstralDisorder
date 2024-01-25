using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPickup : Interactable
{
    public float staminaValue = 10f;
    public override void OnInteract()
    {
        base.OnInteract();
        var player = OmnicatLabs.CharacterControllers.CharacterController.Instance;
        if (player.currentStamina + staminaValue > player.maxStamina)
        {
            player.ChangeStamina(player.maxStamina);
        }
        else
        {
            player.ChangeStamina(player.currentStamina + staminaValue);
        }

        Destroy(gameObject.transform.parent.gameObject);
    }
}
