using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;

    [SerializeField] private Image iconItem;

    [SerializeField] private TextMeshProUGUI quantityText;

    private void Start()
    {
        quantityText.gameObject.SetActive(false);
        inventory = FindObjectOfType<Inventory>();
    }

    public void AddIcon(ItemData itemData)
    {
        iconItem.sprite = itemData.icon;
        iconItem.enabled = true;

        if(itemData.onlySlot)
        {
            quantityText.gameObject.SetActive(true);
            quantityText.text = "x" + inventory.quantityOfItems[itemData.id].ToString();
        }
    }

    public void ClearIcon()
    {
        iconItem.sprite = null;
        iconItem.enabled = false;
        quantityText.gameObject.SetActive(false);
    }
}
