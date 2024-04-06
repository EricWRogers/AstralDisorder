using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class additem : MonoBehaviour
{
    public InventoryItemData item;
    public InventorySystem inventory;
   public void OnClick()
    {
        inventory.Add(item);
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            inventory.Add(item);
        }
    }
   */
}
