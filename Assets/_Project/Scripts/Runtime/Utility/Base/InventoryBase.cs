using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected GameObject panel;
    [SerializeField] protected Transform slotsGroup;
    [SerializeField] public Slot[] slots;
    [SerializeField] protected List<ItemData> items;
    [SerializeField] protected List<ItemData> itemsUsable;
    [SerializeField] protected ItemData temp;

    public int[] quantityOfItems;

    public virtual void Start()
    {
        slots = slotsGroup.GetComponentsInChildren<Slot>();
        panel.SetActive(false);
        UpdateInventory();
    }

    protected void UpdateInventory()
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

    public void AddItemUsable(ItemData item)
    {
        itemsUsable.Add(item);
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
                quantityOfItems[item.id] = item.quantity;
                items.Add(item);
            }
            else
            {
                quantityOfItems[item.id] += item.quantity;
            }
        }

        UpdateInventory();
    }

    public void RemoveItem(ItemData item)
    {
        temp = null;

        if (item.onlySlot)
        {
            quantityOfItems[item.id] -= item.quantity;

            if (quantityOfItems[item.id] <= 0)
            {
                items.Remove(item);
                itemsUsable.Remove(temp);

                print("removi");
            }
        }
        else
        {
            items.Remove(item);
        }

        UpdateInventory();
    }

    public bool HasSlot()
    {
        return items.Count < slots.Length;
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }
}
