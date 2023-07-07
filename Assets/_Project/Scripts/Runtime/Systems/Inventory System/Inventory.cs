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

    public int itemId;

    public string[] KeyCodes;

    private void Start()
    {
        slots = slotsGroup.GetComponentsInChildren<Slot>();
        UpdateInventory();
    }

    private void Update()
    {
        OpenCloseInventory();

        if (itemsUsable.Count > 0)
        {
            UseItemInventory();
        }

    }

    private void OpenCloseInventory()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            panelInventory.SetActive(!panelInventory.activeSelf);
        }
    }

    private void UseItemInventory()
    {
        foreach (var k in KeyCodes)
        {
            if (Input.GetKeyDown(k))
            {
                itemId = Array.IndexOf(KeyCodes, k);
            }
        }


        for(int i = 0;i < itemsUsable.Count;i++)
        {
            if (Input.GetKeyDown(KeyCodes[itemId]) && HasItem(itemsUsable[i]))
            {
               

                if (SameID(itemId))
                {
                    print("TA INDO");

                    itemsUsable[i].UseItem();
                    RemoveItem(itemsUsable[i]);
                    itemsUsable.Remove(itemsUsable[i]);
                    break;
                }
                else
                {
                    i++;
                    print("TA ");
                    return;
                }
            }
        }

        foreach (var item in itemsUsable)
        {
           

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
            print(value);
            if (item.id == value)
            {
                return true;
            }
            else
            {
                print("NAOE");
            }
        }

        return false;
    }
}
