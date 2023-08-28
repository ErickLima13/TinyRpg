using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnCloseInventoryEvent;

    [SerializeField] private GameObject panelInventory;

    [SerializeField] private Transform slotsGroup;

    [SerializeField] private Slot[] slots;

    [SerializeField] private List<ItemData> items;
    [SerializeField] private List<ItemData> itemsUsable;

    public int[] quantityOfItems;

    [SerializeField] private int keyId;

    [SerializeField] private string[] KeyCodes;

    [SerializeField] private ItemData temp;

    [SerializeField] private PanelConfirm panelConfirm;

    [SerializeField] private bool canRemove;

    [SerializeField] private int _coins;

    private PlayerPhysics playerPhysics;

    private void Start()
    {
        slots = slotsGroup.GetComponentsInChildren<Slot>();
        UpdateInventory();
        panelInventory.SetActive(false);
        playerPhysics = GetComponent<PlayerPhysics>();
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

        if (!panelInventory.activeSelf)
        {
            OnCloseInventoryEvent?.Invoke();
            GameStateController.ChangeState(GameState.Gameplay);
        }
        else
        {
            GameStateController.ChangeState(GameState.Inventory);
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
        if (Input.GetKeyDown(KeyCodes[keyId]) && panelInventory.activeSelf)
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
            if (quantityOfItems[item.id] == 0)
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

    public void RemoveQuantityItems(int quantity, ItemData item)
    {
        for (int i = quantity; i > 0; i--)
        {
            RemoveItem(item);
        }
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }

    public bool HasQuantityItems(ItemData item, int quantity)
    {
        return HasItem(item) && quantityOfItems[item.id] >= quantity;
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

    public void ClickItem(ItemData item, bool value)
    {
        temp = item;
        canRemove = value;

        if (!value)
        {
            panelConfirm.PanelConfirmUse(temp);
        }
        else
        {
            panelConfirm.PanelConfirmRemove(temp);
        }
    }

    public void ButtonDrop()
    {
        Vector3 targetPos = Vector3.zero;
        float x = 0;
        float y = 0;

        if (!playerPhysics.IsEmpty())
        {
            switch (playerPhysics.GetIdDirection())
            {
                case 0:
                    x = 0;
                    y = 0.15f;
                    break;
                case 1:
                    x = 0;
                    y = -0.25f;
                    break;
                case 2:
                    if (playerPhysics.isLeft)
                    {
                        x = -0.15f;                        
                    }
                    else
                    {
                        x = 0.15f;
                    }
                    y = -0.1f;
                    break;
            }

            targetPos = new Vector3(x, y, 0);

            Instantiate(temp.itemPrefab, transform.position + targetPos, transform.localRotation);
            RemoveItem(temp);
        }
    }

    public void ButtonConfirm()
    {
        panelConfirm.ClosePanel();
        OpenOrCloseInventory(false);

        if (canRemove)
        {
            RemoveItem(temp);
        }
        else
        {
            temp.UseItem();
            RemoveItem(temp);
        }

    }

    public void OpenOrCloseInventory(bool value)
    {
        panelInventory.SetActive(value);
    }

    public bool HasCoins(ItemData item)
    {
        if (_coins >= item.price)
        {
            return true;
        }

        return false;
    }

    public void CoinsManager(int value)
    {
        _coins += value;
    }
}
