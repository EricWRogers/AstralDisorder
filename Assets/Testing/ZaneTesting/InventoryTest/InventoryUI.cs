using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject slotPrefab;

    List<InventorySlot> slots = new List<InventorySlot>();
    public bool show = false;

    // Start is called before the first frame update
    void Start()
    {
        itemsParent.gameObject.SetActive(false);

        InventorySystem.Instance.onItemChangedCallBack += UpdateUI;

       // slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        PostPlay();

        for (int i = 0; i < slots.Count; i++)
        {
            Debug.Log("Hidden");
            slots[i].ClearSlot();
            slots[i].GetComponentInChildren<Image>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            UpdateUI();
        }
    }

    void PostPlay()
    {
        //itemsParent.gameObject.SetActive(true);
        show = true;
    }

    void UpdateUI()
    {
        if (InventorySystem.Instance.inventory.Count == 0)
        {
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }
            slots.Clear();
        }

        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        Debug.Log(slots.Count);

        foreach (var item in InventorySystem.Instance.inventory)
        {
            var newSlot = Instantiate(slotPrefab, itemsParent);
            slots.Add(newSlot.GetComponent<InventorySlot>());
            newSlot.GetComponentInChildren<Image>().enabled = true;
            newSlot.GetComponentInChildren<InventorySlot>().Additem(item.Data);
        }
        /*
        for (int i = 0; i < InventorySystem.Instance.inventory.Count; i++)
        {
            if (slots.Count < i + 1)
            {
                var newSlot = Instantiate(slotPrefab, itemsParent);
                slots.Add(newSlot.GetComponent<InventorySlot>());
            }
            else if (slots.Count > i + 1)
            {
                Destroy(slots[i]);
                slots.RemoveAt(i);
            }

            if (slots.Count == InventorySystem.Instance.inventory.Count)
            {
                slots[i].GetComponentInChildren<Image>().enabled = true;
                slots[i].Additem(InventorySystem.Instance.inventory[i].Data);
            }
        }*/

        if (slots.Count >= 1)
        {
            itemsParent.gameObject.SetActive(true);
        }
        else
        {
            itemsParent.gameObject.SetActive(false);
        }
    }
}
