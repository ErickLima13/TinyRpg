using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private PlayerPhysics player;

    [SerializeField] private List<Transform> centerRooms;

    private void Start()
    {
        virtualCamera= GetComponent<CinemachineVirtualCamera>();
        player = FindObjectOfType<PlayerPhysics>();

        player.OnWarpPlayer += MoveCam;
    }

    private void MoveCam()
    {
        virtualCamera.Follow = centerRooms[0];
        player._isDoor = false;
    }

    private void OnDisable()
    {
        player.OnWarpPlayer -= MoveCam;
    }
}
