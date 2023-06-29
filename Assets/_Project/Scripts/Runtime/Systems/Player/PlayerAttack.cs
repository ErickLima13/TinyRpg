using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int idPrefab;

    private PlayerPhysics playerPhysics;

    [SerializeField] private GameObject[] fireBallPrefabs;

    private void Start()
    {
        idPrefab = 1;
        playerPhysics = GetComponent<PlayerPhysics>();
    }

    public void FireBallAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void EndAttack()
    {
        StartCoroutine(EndAttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        GameObject temp = Instantiate(fireBallPrefabs[idPrefab], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Destroy(temp, 0.5f);
    }

    private IEnumerator EndAttackCoroutine()
    {
        yield return new WaitForSeconds(0.3f);

        playerPhysics._isAttack = false;
        playerPhysics._speed = 0.7f;
    }
}
