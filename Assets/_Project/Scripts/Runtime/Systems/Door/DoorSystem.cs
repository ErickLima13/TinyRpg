using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private List<DoorWarp> warpList =new();

    private void Start()
    {
        DoorWarp[] temps = FindObjectsOfType<DoorWarp>();

        warpList.AddRange(temps);
    }
}
