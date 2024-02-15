using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItemData : ScriptableObject
{
    //public string name = "New Item"; 
    public string id;
    public string displayName;
    public Sprite icon = null;
    public GameObject prefab;
    public UnityEvent onAddToInventory = new UnityEvent();
    public bool isDefaultItem = false;

    public virtual void Use ()
    {
        
        
        Debug.Log("Using " + name);
    }
}
