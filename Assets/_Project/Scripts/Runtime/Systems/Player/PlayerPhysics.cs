using UnityEngine;
using System;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;

    private Vector2 _inputs;

    [SerializeField] private float _speed;

    [SerializeField] private Transform _rayPos;
    [SerializeField] private LayerMask _doorLayer;

    public bool _isDoor;
    
    private void Initialization()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
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
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");

        _playerRb.velocity = _inputs * _speed;

        Flip();
    }

    private void Flip()
    {
        if (_inputs.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(_inputs.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    private bool Walking()
    {
        if(_inputs.x != 0 || _inputs.y != 0)
        {
            return true;
        }

        return false;
    }

    private void WalkingUp()
    {
        if (_inputs.y > 0)
        {
            _playerAnimator.SetLayerWeight(1, 1);
            _playerAnimator.SetLayerWeight(2, 0);
        }
        else if(_inputs.y < 0)
        {
            _playerAnimator.SetLayerWeight(1, 0);
            _playerAnimator.SetLayerWeight(2, 0);
        }
    }

    private void WalkSideways()
    {
        if (_inputs.y == 0 && _inputs.x != 0)
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
