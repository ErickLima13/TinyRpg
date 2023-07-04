using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image iconItem;

    public void AddIcon(ItemData itemData)
    {
        iconItem.sprite = itemData.icon;
        iconItem.enabled = true;
    }

    public void ClearIcon()
    {
        iconItem.sprite = null;
        iconItem.enabled = false;
    }
}
