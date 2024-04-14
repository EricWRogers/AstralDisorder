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
        itemsParent.gameObject.SetActive(true);
        show = true;
    }

    void UpdateUI()
    {
        for (int i = 0; i < InventorySystem.Instance.inventory.Count; i++)
        {
            if (slots.Count < i + 1)
            {
                var newSlot = Instantiate(slotPrefab, itemsParent);
                slots.Add(newSlot.GetComponent<InventorySlot>());
            }

            slots[i].GetComponentInChildren<Image>().enabled = true;
            slots[i].Additem(InventorySystem.Instance.inventory[i].Data);
        }


        //for (int i = 0; i < slots.Length; i++)
        //{
        //    if (i < InventorySystem.Instance.inventory.Count)
        //    {
        //        if (slots[i].icon.enabled == false)
        //        {
        //            slots[i].GetComponentInChildren<Image>().enabled = true;
        //            Debug.Log("Revealed");
        //        }

        //        slots[i].Additem(InventorySystem.Instance.inventory[i].Data);
        //    }

        //}
    }
}
