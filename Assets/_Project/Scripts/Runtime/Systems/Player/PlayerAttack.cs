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

        foreach (GameObject fireBall in fireBallPrefabs)
        {
            fireBall.SetActive(false);
        }
    }

    public void FireBallAttack()
    {
        fireBallPrefabs[idPrefab].SetActive(true);
    }

    public void EndAttack()
    {
        fireBallPrefabs[idPrefab].SetActive(false);
        playerPhysics._speed = 0.7f;
    }
}
