using System.Collections;
using UnityEngine;

public class IAmovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speed;

    [SerializeField] private float _waitTime;
    [SerializeField] private float _moveTime;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Movement());
    }

    private void Update()
    {
        _rigidBody2D.velocity = _direction * _speed;
    }

    private IEnumerator Movement()
    {
        yield return new WaitForSeconds(Random.Range(0, _waitTime));

        float x = Random.Range(-1, 2);
        float y = Random.Range(-1, 2);
        _direction = new Vector2(x, y);

        yield return new WaitForSeconds(Random.Range(1, _moveTime));

        _direction = Vector2.zero;
        StartCoroutine(Movement());
    }

}
