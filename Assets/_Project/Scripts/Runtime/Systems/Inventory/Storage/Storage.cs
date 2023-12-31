public class Storage : InventoryBase
{
    // [Header("Storage")]

    private Inventory _inventory;


    public override void Start()
    {
        base.Start();
        _inventory = GetComponent<Inventory>();
    }

    public void OpenCloseStorage(bool value)
    {
        _inventory.OpenOrCloseInventory(value);
        panel.SetActive(value);

        foreach (Slot s in _inventory.slots)
        {
            if (s.HasItem())
            {
                s.EnableDisableRemoveButton(!value);
            }
        }
    }

    public void PassItemToStorage(ItemData item)
    {
        temp = item;

        if (HasSlot())
        {
            TakeItem(temp);
        }

        if (temp.usable)
        {
            AddItemUsable(temp);
        }
    }




}
