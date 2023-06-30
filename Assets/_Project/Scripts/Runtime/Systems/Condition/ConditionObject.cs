using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionObject : MonoBehaviour
{
    [SerializeField] private ConditionController controller;

    private Status status;

    private void OnDisable()
    {
        status.OnEnemieDie -= EnemieDie;
    }

    private void Start()
    {
        status = GetComponent<Status>();
        controller.AddObject(gameObject);

        status.OnEnemieDie += EnemieDie;
    }

    private void EnemieDie()
    {
        controller.RemoveObject(gameObject);
    }

}
