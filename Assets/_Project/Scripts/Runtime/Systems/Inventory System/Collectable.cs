using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        itemData.usableItem = GetComponent<IUsableItem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Inventory inventory))
        {
            if (inventory.HasSlot())
            {
                inventory.TakeItem(itemData);
                gameObject.SetActive(false);
            }

            if (itemData.usable)
            {
                inventory.AddItemUsable(itemData);
            }
        }
    }
}
