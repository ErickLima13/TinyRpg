using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRoom : MonoBehaviour
{
    public int indexCenter;

    [SerializeField] private VirtualCamera _virtualCam;

    private void Start()
    {
        _virtualCam = FindObjectOfType<VirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerPhysics playerCol))
        {
            _virtualCam.MoveCam(indexCenter);
        }
    }

}
