using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D _playerRb;

    public PlayerAttack _playerAttack;

    public Vector2 _inputs;

    public float _speed;

    [SerializeField] private Transform _rayPos;
    [SerializeField] private LayerMask _doorLayer;
    [SerializeField] private LayerMask _emptyLayer;

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
        if (GameStateController._currentState == GameState.Gameplay)
        {
            Movement();
        }     
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

    public RaycastHit2D IsEmpty()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _inputs, 0.27f, _emptyLayer);
        Debug.DrawRay(transform.position,_inputs * 0.27f,Color.white);

        if (hit)
        {
            print(hit.transform.name);
        }

        return hit;
    }
}
