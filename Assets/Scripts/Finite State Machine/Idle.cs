using UnityEngine;
using UnityEngine.AI;

public class Idle : IState
{
    private readonly Entity _entity;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;

    private float _returnHomeTimer;
    private float _returnHomeTime;
    private float _patrolTime;
    private float _timeToActivatePatrol;

    public Idle(Entity entity)
    {
        _entity = entity;
        _animator = _entity.Animator;
        _navMeshAgent = _entity.NavAgent;
        _returnHomeTime = _entity.ReturnHomeTime;
        _rigidbody = _entity.Rigidbody;
    }

    public void Tick()
    {
        UpdateReturnHomeTime();
    }

    public void OnEnter()
    {
        _returnHomeTimer = 0;
        _patrolTime = 0;

        _navMeshAgent.enabled = false;
        _animator.SetBool("Running", false);
        _timeToActivatePatrol = 3;
    }

    public void OnExit()
    {
        _patrolTime = 0;
    }

    public bool UpdateReturnHomeTime()
    {
        _returnHomeTimer += Time.deltaTime;

        if (_returnHomeTimer >= _returnHomeTime)
        {
            _returnHomeTimer = _returnHomeTime;
            return true;
        }

        return false;
    }

    public bool TogglePatrol()
    {
        if (!_entity.StateMachine.IsHome) return false;

        _patrolTime += Time.deltaTime;
        if (_patrolTime >= _timeToActivatePatrol) return true;

        return false;
    }
}