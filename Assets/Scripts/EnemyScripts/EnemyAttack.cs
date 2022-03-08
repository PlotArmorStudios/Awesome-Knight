using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount = 10f;
    Transform _playerTarget;
    Animator _animator;

    bool finishedAttack = true;
    float damageDistance = 2f;

    private Entity _entity;
    
    PlayerHealth _playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        _entity = GetComponent<Entity>();
        _playerHealth = _entity.PlayerTarget.GetComponent<PlayerHealth>();
        _animator = GetComponent<Animator>();

        _playerHealth = _playerTarget.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedAttack)
        {
            if (_playerTarget)
            {
                DealDamage(CheckIfAttacking());
            }
        }
        else
        {
            if (!_animator.IsInTransition(0) && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finishedAttack = true;
            }
        }
    }

    bool CheckIfAttacking()
    {
        bool isAttacking = false;

        if (!_animator.IsInTransition(0) && _animator.GetCurrentAnimatorStateInfo(0).IsName("Atk1") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Atk2"))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                isAttacking = true;
                finishedAttack = false;
            }
        }

        return isAttacking;
    }

    void DealDamage(bool isAttacking)
    {
        if (Vector3.Distance(transform.position, _playerTarget.position) <= damageDistance)
        {
            _playerHealth.TakeDamage(damageAmount);
        }
    }
}