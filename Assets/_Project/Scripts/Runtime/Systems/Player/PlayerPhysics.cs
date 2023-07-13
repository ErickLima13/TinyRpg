using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D _playerRb;

    public PlayerAttack _playerAttack;

    public Vector2 _inputs;

    public float _speed;

    [SerializeField] private Transform _rayPos;
    [SerializeField] private LayerMask _doorLayer;

    private void Initialization()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    void Start()
    {
        Initialization();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");

        _playerRb.velocity = _inputs * _speed;

        Flip();
    }

    private void Flip()
    {
        if (_inputs.x < 0)
        {
            _playerAttack.idPrefab = 2;
            transform.localScale = new(-1, 1, 1);
        }
        else if (_inputs.x > 0)
        {
            _playerAttack.idPrefab = 2;
            transform.localScale = Vector3.one;
        }
    }
}
