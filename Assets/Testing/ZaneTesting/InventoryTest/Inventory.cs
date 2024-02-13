using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//find pickup script and put "bool wasPickedUp = Inventory.instance.Add(item);" in the Pickup() function

//if (wasPickedUp)
//    Destroy(gameObject);

public class Inventory : MonoBehaviour
{

#region Singleton

    public static Inventory instance;

    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 20;

    public List<InventoryItemData> items = new List<InventoryItemData>();

    public bool Add (InventoryItemData item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }

        return true;
    }

    public void Remove (InventoryItemData item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }


}
