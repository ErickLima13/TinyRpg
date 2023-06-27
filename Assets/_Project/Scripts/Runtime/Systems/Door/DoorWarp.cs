using UnityEngine;
public class DoorWarp : MonoBehaviour
{
    [SerializeField] private Transform exitPos;

    [SerializeField] private PlayerPhysics player;

    private void OnEnable()
    {
        player.OnWarpPlayer += WarpPlayer;
    }

    private void OnDisable()
    {
        player.OnWarpPlayer -= WarpPlayer;
    }

    private void WarpPlayer()
    {
        print("chamei " + name);
        player.transform.position = exitPos.position;
        player._isDoor = false;
    }


}
