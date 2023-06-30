using UnityEngine;

public class Status : MonoBehaviour
{
    public event Action OnEnemieDie;

    [SerializeField] private GameObject hitPrefab;

    public int maxLife;

    public void HealthChange(int value)
    {
        GameObject temp = Instantiate(hitPrefab, transform.position, Quaternion.identity);
        Destroy(temp, 0.5f);
        maxLife -= value;

        if (maxLife <= 0)
        {
            OnEnemieDie?.Invoke();
            Destroy(gameObject, 0.1f);

        }
    }
}