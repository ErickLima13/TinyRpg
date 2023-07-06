using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/New Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public int quantity;
    public bool onlySlot;
    public Sprite icon;
}
