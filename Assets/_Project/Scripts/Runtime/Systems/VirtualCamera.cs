using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private PlayerPhysics player;

    [SerializeField] private List<Transform> centerRooms;

    private void OnEnable()
    {
        player.OnWarpPlayer += MoveCam;
    }

    private void OnDisable()
    {
        player.OnWarpPlayer -= MoveCam;
    }

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void MoveCam()
    {
        virtualCamera.Follow = centerRooms[0];
        player._isDoor = false;
    }


}
