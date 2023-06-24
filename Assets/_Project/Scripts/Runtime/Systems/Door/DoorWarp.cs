using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorWarp : MonoBehaviour
{
    [SerializeField] private Transform exit;

    [SerializeField] private PlayerPhysics player;

    private void Start()
    {
        player = FindObjectOfType<PlayerPhysics>();
        player.OnWarpPlayer += WarpPlayer;
    }

    private void WarpPlayer()
    {
        player.transform.position = exit.position;
    }

    private void OnDisable()
    {
        player.OnWarpPlayer -= WarpPlayer;
    }
}
