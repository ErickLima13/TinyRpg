using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerPhysics _playerPhysics;

    private void Initialization()
    {
        _animator = GetComponent<Animator>();
        _playerPhysics = GetComponent<PlayerPhysics>();
    }

    private void Start()
    {
        Initialization();
    }

    void Update()
    {
        UpdateAnimator();
    }

    private bool Walking()
    {
        if (_playerPhysics._inputs.x != 0 || _playerPhysics._inputs.y != 0)
        {
            return true;
        }

        return false;
    }

    private void WalkingUp()
    {
        if (_playerPhysics._inputs.y > 0)
        {
            _playerPhysics._playerAttack.idPrefab = 0;
            _animator.SetLayerWeight(1, 1);
            _animator.SetLayerWeight(2, 0);
        }
        else if (_playerPhysics._inputs.y < 0)
        {
            _playerPhysics._playerAttack.idPrefab = 1;
            _animator.SetLayerWeight(1, 0);
            _animator.SetLayerWeight(2, 0);
        }
    }

    private void WalkSideways()
    {
        if (_playerPhysics._inputs.y == 0 && _playerPhysics._inputs.x != 0)
        {
            _animator.SetLayerWeight(2, 1);
        }
    }

    private void UpdateAnimator()
    {
        _animator.SetBool("Walk", Walking());
        WalkingUp();
        WalkSideways();
        Attack();
      
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Attack");
            _playerPhysics._speed = 0;
        }
    }
}
