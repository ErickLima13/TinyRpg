using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private GameObject hitPrefab;

    public int maxLife;

    public void HealthChange(int value)
    {
        maxLife -= value;

        if (maxLife <= 0)
        {
            GameObject temp = Instantiate(hitPrefab, transform.position, Quaternion.identity);
            Destroy(temp, 0.5f);
            Destroy(gameObject);
        }
    }
}