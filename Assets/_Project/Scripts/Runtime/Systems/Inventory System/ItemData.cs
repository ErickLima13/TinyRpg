using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/New Item")]
public class ItemData : ScriptableObject
{
    public string nameItem;
    public Sprite icon;
}
