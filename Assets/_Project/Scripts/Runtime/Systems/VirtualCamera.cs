using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private MiniMapController _miniMapController;

    [SerializeField] private List<Transform> centerRooms;

    [SerializeField] private Animator fade;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _miniMapController = FindObjectOfType<MiniMapController>();
    }

    public void MoveCam(int value)
    {
        fade.Play(0);
        virtualCamera.m_Follow = centerRooms[value];
        _miniMapController.UpdateMap(value);
    }


}
