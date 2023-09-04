using System;
using UnityEngine;

public class Inventory : InventoryBase
{
    public event Action OnCloseInventoryEvent;

    [Header("Inventory")]
    [SerializeField] private int keyId;

    [SerializeField] private string[] KeyCodes;

    [SerializeField] private PanelConfirm panelConfirm;

    [SerializeField] private bool canRemove;

    [SerializeField] private int _coins;

    private PlayerPhysics playerPhysics;

    public override void Start()
    {
        base.Start();
        UpdateInventory();
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
        if (GameStateController._currentState == GameState.Chest)
        {
            return;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            OpenOrCloseInventory(!panel.activeSelf);
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
        if (Input.GetKeyDown(KeyCodes[keyId]) && panel.activeSelf)
        {
            if (SameID(keyId))
            {
                temp.UseItem();
                RemoveItem(temp);
            }
        }
    }

    public void RemoveQuantityItems(int quantity, ItemData item)
    {
        for (int i = quantity; i > 0; i--)
        {
            RemoveItem(item);
        }
    }

    public bool HasQuantityItems(ItemData item, int quantity)
    {
        return HasItem(item) && quantityOfItems[item.id] >= quantity;
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
            GameStateController.ChangeState(GameState.Gameplay);
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
            RemoveItem(temp);
            temp.UseItem();
        }

        

    }

    public void OpenOrCloseInventory(bool value)
    {
        panel.SetActive(value);

        if (!value)
        {
            OnCloseInventoryEvent?.Invoke();
            GameStateController.ChangeState(GameState.Gameplay);
        }
        else
        {
            GameStateController.ChangeState(GameState.Inventory);
        }
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
