using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Inventory inventory))
        {
            inventory.TakeItem(itemData);
            Destroy(gameObject, 0.1f);
        }
    }
}
