using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject panelInventory;

    [SerializeField] private Transform slotsGroup;

    [SerializeField] private Slot[] slots;

    public List<ItemData> items;

    private void Start()
    {
        slots = slotsGroup.GetComponentsInChildren<Slot>();
        UpdateInventory();
    }

    private void Update()
    {
        OpenCloseInventory();
    }

    private void OpenCloseInventory()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            panelInventory.SetActive(!panelInventory.activeSelf);
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

        items.Add(item);
        UpdateInventory();


    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
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
}