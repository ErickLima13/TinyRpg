using UnityEngine;

public class DoorWarp : MonoBehaviour
{
    [SerializeField] private Transform exitPos;

    [SerializeField] private PlayerPhysics player;

    private DoorController controller;

    private void Start()
    {
        controller = GetComponentInParent<DoorController>();
    }

    private void WarpPlayer()
    {
        if (!controller.needKey)
        {
            player.transform.position = exitPos.position;
            player._isDoor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerPhysics playerCol))
        {
            player = playerCol;
            CheckPlayerHasKey();
            WarpPlayer();
        }
    }

    private void CheckPlayerHasKey()
    {
        if (player.keys > 0 && controller.needKey)
        {
            controller.needKey = false;
            controller.UpddateDoor();
            player.keys--;
        }
    }
}
