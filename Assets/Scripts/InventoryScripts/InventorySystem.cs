using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public static InventorySystem Instance;

    public List<InventoryItem> inventory;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    private Dictionary<InventoryItemData, InventoryItem> savedDictionary = new Dictionary<InventoryItemData, InventoryItem>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        inventory = new List<InventoryItem>();
        SaveManager.Instance.onSave.AddListener(HandleSave);
        SaveManager.Instance.onReset.AddListener(HandleReset);
    }

    private void HandleSave()
    {
        Debug.Log($"Saving Count: {inventory.Count}");
        foreach(var item in m_itemDictionary)
        {
            if (!savedDictionary.ContainsKey(item.Key))
            {
                savedDictionary.Add(item.Key, item.Value);
            }
        }
    }

    private void HandleReset()
    {
        m_itemDictionary.Clear();
        inventory.Clear();
        foreach (var item in savedDictionary)
        {
            Add(item.Key);
        }
        Debug.Log($"Reset Count: {inventory.Count}");

    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public InventoryItem Get(string name)
    {
        var item = inventory.Find(item => item.Data.displayName == name);
        if (item != null)
        if (m_itemDictionary.TryGetValue(item.Data, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData referenceData)
    {
        referenceData.onAddToInventory.Invoke();

        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

}
[Serializable]
public class InventoryItem
{
    public InventoryItemData Data;
    public int stackSize;

    public InventoryItem(InventoryItemData source)
    {
        Data = source;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}