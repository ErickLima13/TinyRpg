using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory _inventory;
    private Storage _storage;

    [SerializeField] private ItemData _item;

    [SerializeField] private Image _iconItem;

    [SerializeField] private TextMeshProUGUI _quantityText;

    [SerializeField] private Button _removeButton;

    [SerializeField] private bool isStorage;


    private void Start()
    {
        _quantityText.gameObject.SetActive(false);
        _inventory = FindObjectOfType<Inventory>();
        _storage = FindObjectOfType<Storage>();
    }

    public void AddIcon(ItemData itemData)
    {
        _item = itemData;
        _iconItem.sprite = itemData.icon;
        _iconItem.enabled = true;

        if (itemData.missionItem)
        {
            EnableDisableRemoveButton(false);
        }
        else
        {
            EnableDisableRemoveButton(true);
        }

        if (itemData.onlySlot)
        {
            _quantityText.gameObject.SetActive(true);

            if (isStorage)
            {
                _quantityText.text = "x" + _storage.quantityOfItems[itemData.id].ToString();
            }
            else
            {
                _quantityText.text = "x" + _inventory.quantityOfItems[itemData.id].ToString();
            }
        }
        else
        {
            _quantityText.gameObject.SetActive(false);
        }
    }

    public void ClearIcon()
    {
        _item = null;
        _quantityText.gameObject.SetActive(false);
        _iconItem.sprite = null;
        _iconItem.enabled = false;
        EnableDisableRemoveButton(false);
    }

    public void UseItemButton()
    {
        if (GameStateController._currentState != GameState.Chest)
        {
            if (_item != null && _item.usable)
            {
                _inventory.ClickItem(_item, false);
            }
        }
        else if (GameStateController._currentState == GameState.Chest)
        {
            if (!isStorage)
            {
                _storage.PassItemToStorage(_item);
                _inventory.RemoveItem(_item);
            }
            else
            {
                _inventory.TakeItem(_item);
                _storage.RemoveItem(_item);
            }


        }
    }

    public void RemoveItemButton()
    {
        if (_item != null)
        {
            _inventory.ClickItem(_item, true);
        }
    }

    public void EnableDisableRemoveButton(bool enabled)
    {
        _removeButton?.gameObject.SetActive(enabled);
    }

    public bool HasItem()
    {
        return _item != null;
    }
}
