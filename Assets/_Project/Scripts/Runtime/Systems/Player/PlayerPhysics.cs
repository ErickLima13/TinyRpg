using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;
    private SpriteRenderer _playerSpriteRenderer;


    private Vector2 inputs;

    [SerializeField] private float speed;

    private void Initialization()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Initialization();
    }

    void Update()
    {
        Movement();

        UpdateAnimator();
    }

    private void Movement()
    {
        inputs.x = Input.GetAxisRaw("Horizontal");
        inputs.y = Input.GetAxisRaw("Vertical");

        _playerRb.velocity = inputs * speed;

        Flip();
    }

    private void Flip()
    {
        if (inputs.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(inputs.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    private bool Walking()
    {
        if(inputs.x != 0 || inputs.y != 0)
        {
            return true;
        }

        return false;
    }

    private void WalkingUp()
    {
        if (inputs.y > 0)
        {
            _playerAnimator.SetLayerWeight(1, 1);
            _playerAnimator.SetLayerWeight(2, 0);
        }
        else if(inputs.y < 0)
        {
            _playerAnimator.SetLayerWeight(1, 0);
            _playerAnimator.SetLayerWeight(2, 0);
        }
    }

    private void WalkSideways()
    {
        if (inputs.y == 0 && inputs.x != 0)
        {
            _playerAnimator.SetLayerWeight(2, 1);
        }
    }

    private void UpdateAnimator()
    {
        _playerAnimator.SetBool("Walk", Walking());
        WalkingUp();
        WalkSideways();
        Attack();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _playerAnimator.SetTrigger("Attack");
        }
       
    }

}
