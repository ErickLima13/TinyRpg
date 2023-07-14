using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;

    private ItemData item;

    [SerializeField] private Image iconItem;

    [SerializeField] private TextMeshProUGUI quantityText;

    [SerializeField] private Button removeButton;

    private void Start()
    {
        quantityText.gameObject.SetActive(false);
        inventory = FindObjectOfType<Inventory>();
    }

    public void AddIcon(ItemData itemData)
    {
        item = itemData;
        iconItem.sprite = itemData.icon;
        iconItem.enabled = true;

        if (itemData.missionItem)
        {
            removeButton.gameObject.SetActive(false);
        }
        else
        {
            removeButton.gameObject.SetActive(true);
        }

        if (itemData.onlySlot)
        {
            quantityText.gameObject.SetActive(true);
            quantityText.text = "x" + inventory.quantityOfItems[itemData.id].ToString();
        }
        else
        {
            quantityText.gameObject.SetActive(false);
        }
    }

    public void ClearIcon()
    {
        item = null;
        quantityText.gameObject.SetActive(false);
        iconItem.sprite = null;
        iconItem.enabled = false;

        removeButton.gameObject.SetActive(false);

    }

    public void UseItemButton()
    {
        if (item != null && item.usable)
        {
            item.UseItem();
            inventory.RemoveItem(item);
        }
    }

    public void RemoveItemButton()
    {
        if(item != null)
        {
            inventory.RemoveItem(item);
        }
        
    }
}
