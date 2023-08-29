using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected GameObject panel;
    [SerializeField] protected Transform slotsGroup;
    [SerializeField] protected Slot[] slots;
    [SerializeField] protected List<ItemData> items;
    [SerializeField] protected List<ItemData> itemsUsable;

    public int[] quantityOfItems;

    public virtual void Start()
    {
        slots = slotsGroup.GetComponentsInChildren<Slot>();
        panel.SetActive(false);
    }
}
