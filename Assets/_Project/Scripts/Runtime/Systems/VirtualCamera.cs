using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private List<Transform> centerRooms;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void MoveCam(int value)
    {
        virtualCamera.Follow = centerRooms[value];
    }


}
