using UnityEngine;
using OmnicatLabs.Audio;

public class WeaponPickup : Interactable
{
    public GameObject weaponPrefab;

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
        //to unhide the arms for the first time picking up a weapon. doesn't hurt to be called every time
        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(false, false, false);
        WeaponSwap.Instance.AddNewWeapon(weaponPrefab);

        Destroy(gameObject);
    }
}
