using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/New Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public int quantity;
    public bool onlySlot;
    public bool usable;
    public Sprite icon;

    public IUsableItem usableItem;

    public void UseItem()
    {
        if (usable)
        {
            usableItem.MethodUse();
        }
    }
}
