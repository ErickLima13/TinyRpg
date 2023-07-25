using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotStore : MonoBehaviour
{
    [SerializeField] private ItemData _storeItem;

    [SerializeField] private Image _iconItem;

    [SerializeField] private TextMeshProUGUI _textPrice;

    [SerializeField] private Button _buttonBuy;

    private Inventory inventory;

    private void Initialization()
    {
        inventory = FindObjectOfType<Inventory>();
        _iconItem.sprite = _storeItem.icon;
        _textPrice.text = "$" + _storeItem.price.ToString();
        CheckCoins();
    }

    private void Start()
    {
        Initialization();
    }

    private void CheckCoins()
    {
        if (inventory.HasCoins(_storeItem))
        {
            _buttonBuy.interactable = true;
        }
        else
        {
            _buttonBuy.interactable = false;
        }
    }

    public void BuyItemButton()
    {
        if (inventory.HasCoins(_storeItem) && inventory.HasSlot())
        {
            inventory.CoinsManager(_storeItem.price * -1);
            inventory.TakeItem(_storeItem);
            CheckCoins();
        }
    }

}
