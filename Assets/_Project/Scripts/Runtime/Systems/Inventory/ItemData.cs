using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/New Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public int quantity;
    public int price;
    public bool onlySlot;
    public bool usable;
    public bool missionItem;
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
