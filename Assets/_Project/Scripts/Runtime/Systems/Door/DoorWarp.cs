using UnityEngine;
using UnityEngine.Windows;

public class DoorWarp : MonoBehaviour
{
    [SerializeField] private Transform exitPos;

    [SerializeField] private PlayerPhysics player;

    private void WarpPlayer()
    {
        player.transform.position = exitPos.position;
        player._isDoor = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerPhysics  playerCol))
        {
            player = playerCol;
            WarpPlayer();
        }
    }
}
