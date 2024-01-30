using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    
    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
 
        InventorySystem.Instance.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI ()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < InventorySystem.Instance.inventory.Count)
            {
                slots[i].Additem(InventorySystem.Instance.inventory[i].Data);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
