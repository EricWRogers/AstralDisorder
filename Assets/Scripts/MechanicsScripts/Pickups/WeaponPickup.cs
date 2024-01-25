using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OmnicatLabs.Audio;

public class WeaponPickup : Interactable
{
    protected override void OnHover()
    {
        base.OnHover();

        GetComponent<Dialogue>().TriggerDialogue();
    }

    public override void OnInteract()
    {
        AudioManager.Instance.Play("PickUp");

        base.OnInteract();

        if (TryGetComponent(out ChangeObjective changeObjective))
        {
            changeObjective.Change();
        }

        Destroy(gameObject);
    }
}
