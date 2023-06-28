using UnityEngine;


public class CenterRoom : MonoBehaviour
{
    private VirtualCamera _virtualCam;
   
    [SerializeField] private int indexCenter;

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
