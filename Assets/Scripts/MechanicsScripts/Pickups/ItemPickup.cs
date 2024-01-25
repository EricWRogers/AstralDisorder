using OmnicatLabs.Audio;

public class ItemPickup : Interactable
{

    public InventoryItemData item;   // Item to put in the inventory if picked up

    // When the player interacts with the item
    public override void OnInteract()
    {
        base.OnInteract();

        PickUp();

        if (TryGetComponent<Dialogue>(out var dialogue))
        {
            dialogue.TriggerDialogue();
        }
    }

    // Pick up the item
    void PickUp()
    {
        AudioManager.Instance.Play("PickUp");

        InventorySystem.Instance.Add(item);   // Add to inventory

        gameObject.SetActive(false);    // Destroy item from scene
    }

    public override void OnReset()
    {
        base.OnReset();

        gameObject.SetActive(true);
    }
}