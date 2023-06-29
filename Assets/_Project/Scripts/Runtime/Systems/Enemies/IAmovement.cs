using System.Collections;
using UnityEngine;

public class IAmovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speed;

    [SerializeField] private float _waitTime;
    [SerializeField] private float _moveTime;

    [SerializeField] private bool _viewPlayer;
    [SerializeField] private float distanceView;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Movement());
    }

    private void Update()
    {
        _rigidBody2D.velocity = _direction * _speed;
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private IEnumerator Movement()
    {
        _direction = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(0, _waitTime));

        if (!_viewPlayer)
        {
            float x = Random.Range(-1, 2);
            float y = Random.Range(-1, 2);
            _direction = new Vector2(x, y);

            yield return new WaitForSeconds(Random.Range(1, _moveTime));
        }

        StartCoroutine(Movement());
    }

    private void FindPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, distanceView, playerLayer);

        if (player != null)
        {
            _viewPlayer = true;  
            _direction = Vector3.Normalize(player.transform.position - transform.position);
        }
        else
        {
            _viewPlayer = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceView);
    }



}
