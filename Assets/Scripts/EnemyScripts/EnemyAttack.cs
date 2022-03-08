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
    }

    public void DealDamage()
    {
        _playerHealth.TakeDamage(damageAmount);
    }
}