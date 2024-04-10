using System;
using System.Collections;
using System.Collections.Generic;
using OmnicatLabs.CharacterControllers;
using OmnicatLabs.Audio;
using UnityEngine;

public enum UpgradeIds
{
    MagBoots = 0,
    WallRunning = 1,
    Grapple = 2,
    DoubleJump = 3,
    LightningGun = 4
}

public class UpgradePickup : Interactable
{
    public float rotationSpeed = 45f;
    public UpgradeIds UpgradeID;
    public InventoryItemData item;   // Item to put in the inventory if picked up

    public override void OnInteract()
    {
        AudioManager.Instance.Play("PickUp");

        base.OnInteract();

        if (TryGetComponent<Dialogue>(out var dialogue))
        {
            dialogue.TriggerDialogue();
        }

        switch (UpgradeID)
        {
            case UpgradeIds.MagBoots:
                UpgradeManager.AddToOwned(UpgradeIds.MagBoots);
                break;
            case UpgradeIds.WallRunning:
                OmnicatLabs.CharacterControllers.CharacterController.Instance.wallRunningUnlocked = true;
                UpgradeManager.AddToOwned(UpgradeIds.WallRunning);
                break;
            case UpgradeIds.Grapple:
                OmnicatLabs.CharacterControllers.CharacterController.Instance.grappleUnlocked = true;
                ArmController.Instance.EnableGrapple();
                UpgradeManager.AddToOwned(UpgradeIds.Grapple);
                break;
            case UpgradeIds.DoubleJump:
                UpgradeManager.AddToOwned(UpgradeIds.DoubleJump);
                break;
            case UpgradeIds.LightningGun:
                UpgradeManager.AddToOwned(UpgradeIds.LightningGun);
                break;
        }

        InventorySystem.Instance.Add(item);

        gameObject.SetActive(false);
    }

    public override void OnReset()
    {
        base.OnReset();

        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}