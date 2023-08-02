using UnityEngine;

public class ConditionObject : MonoBehaviour
{
    [SerializeField] private ConditionController controller;

    private Status status;

    private void OnDisable()
    {
        status.OnDie -= EnemieDie;
    }

    private void Start()
    {
        status = GetComponent<Status>();
        controller.AddObject(gameObject);

        status.OnDie += EnemieDie;
    }

    private void EnemieDie()
    {
        controller.RemoveObject(gameObject);
    }

}
