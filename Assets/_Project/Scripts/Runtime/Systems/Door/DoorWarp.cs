using UnityEngine;

public class DoorWarp : MonoBehaviour
{
    [SerializeField] private Transform exitPos;

    private Inventory inventory;

    private DoorController controller;

    private void Start()
    {
        controller = GetComponentInParent<DoorController>();
    }

    private void WarpPlayer()
    {
        if (!controller.needKey)
        {
            inventory.transform.position = exitPos.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller.conditionDoor)
        {
            return;
        }

        if (collision.TryGetComponent(out Inventory inventoryCol))
        {
            inventory = inventoryCol;
            CheckInventoryHasKey();
            WarpPlayer();
        }
    }

    private void CheckInventoryHasKey()
    {
        if (inventory.HasItem(controller.item) && controller.needKey)
        {
            controller.needKey = false;   
            inventory.RemoveItem(controller.item);
        }

        controller.UpddateDoor();
    }
}
