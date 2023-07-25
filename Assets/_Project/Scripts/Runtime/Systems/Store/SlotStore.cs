using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotStore : MonoBehaviour
{


    public ItemData storeItem;
    public Image iconItem;
    public TextMeshProUGUI textPrice;

    public Button buttonBuy;

    private Inventory inventory;

    private void Initialization()
    {
        inventory = FindObjectOfType<Inventory>();

        iconItem.sprite = storeItem.icon;
        textPrice.text = storeItem.price.ToString();

        CheckCoins();
    }

    private void Start()
    {
        Initialization();
    }

    private void CheckCoins()
    {
        if (inventory.HasCoins(storeItem))
        {
            buttonBuy.interactable = true;
        }
        else
        {
            buttonBuy.interactable = false;
        }
    }

    public void BuyItemButton()
    {
        if (inventory.HasCoins(storeItem) && inventory.HasSlot())
        {
            inventory.CoinsManager(storeItem.price * -1);
            inventory.TakeItem(storeItem);
            CheckCoins();
        }
    }

}
