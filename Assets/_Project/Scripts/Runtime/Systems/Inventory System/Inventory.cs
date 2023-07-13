using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject panelInventory;

    [SerializeField] private Transform slotsGroup;

    [SerializeField] private Slot[] slots;

    [SerializeField] private List<ItemData> items;
    [SerializeField] private List<ItemData> itemsUsable;

    public int[] quantityOfItems;

    public int keyId;

    public string[] KeyCodes;

    public ItemData temp;

    private void Start()
    {
        slots = slotsGroup.GetComponentsInChildren<Slot>();
        UpdateInventory();
        panelInventory.SetActive(false);
    }

    private void Update()
    {
        OpenCloseInventory();

        if (itemsUsable.Count > 0)
        {
            UpdateKey();
            UseItemInventory();
        }

    }

    private void OpenCloseInventory()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            panelInventory.SetActive(!panelInventory.activeSelf);
        }

        if (panelInventory.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void UpdateKey()
    {
        foreach (var k in KeyCodes)
        {
            if (Input.GetKeyDown(k))
            {
                keyId = Array.IndexOf(KeyCodes, k);
            }
        }
    }

    private void UseItemInventory()
    {
        if (Input.GetKeyDown(KeyCodes[keyId]))
        {
            if (SameID(keyId))
            {
                temp.UseItem();
                RemoveItem(temp);
               
            }
        }
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddIcon(items[i]);
            }
            else
            {
                slots[i].ClearIcon();
            }
        }
    }

    public void TakeItem(ItemData item)
    {
        if (!item.onlySlot)
        {
            items.Add(item);
        }
        else
        {
            if (!HasItem(item))
            {
                items.Add(item);
            }

            quantityOfItems[item.id] += item.quantity;
        }

        UpdateInventory();
    }

    public void RemoveItem(ItemData item)
    {
        if (item.onlySlot)
        {
            if (quantityOfItems[item.id] <= 0)
            {
                items.Remove(item);
                itemsUsable.Remove(temp);
            }

            quantityOfItems[item.id] -= item.quantity;
        }
        else
        {
            items.Remove(item);
        }

        UpdateInventory();
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }

    public bool HasSlot()
    {
        return items.Count < slots.Length;
    }

    public void AddItemUsable(ItemData item)
    {
        itemsUsable.Add(item);
    }

    public bool SameID(int value)
    {
        foreach (ItemData item in itemsUsable)
        {
            if (item.id == value)
            {
                temp = item;
                return true;
            }
        }

        return false;
    }
}
