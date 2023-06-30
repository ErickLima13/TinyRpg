using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int damageBall;

    [SerializeField] private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Status status))
        {
            status.HealthChange(damageBall);
        }
    }

    public void EndAttack()
    {
        playerAttack.EndAttack();
    }
}
