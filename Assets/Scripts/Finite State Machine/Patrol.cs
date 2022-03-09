//#define PatrolDebug

using UnityEngine;
using UnityEngine.AI;

public class Patrol : IState
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Entity _entity;

    private float _patrolTime;
    private float _timeToPatrol;
    private Vector3 _newDestination;
    private bool _patrolling;

    public Patrol(Entity entity)
    {
        _entity = entity;
        _navMeshAgent = entity.NavAgent;
        _animator = entity.Animator;
    }

    public void Tick()
    {
        PatrolArea();
    }

    public void OnEnter()
    {
        _timeToPatrol = Random.Range(0, 10);
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }

    private void PatrolArea()
    {
        if ((_patrolTime < _timeToPatrol) && _patrolling == false)
            _patrolTime += Time.deltaTime;

        if (_patrolTime >= _timeToPatrol)
        {
            _timeToPatrol = Random.Range(2, 12);
            TriggerPatrol();
            _patrolTime = 0;
        }

        if (Vector3.Distance(_entity.transform.position, _newDestination) < 1f)
        {
            _animator.SetBool("Running", false);
            _patrolling = false;
        }
    }

    private void TriggerPatrol()
    {
        var randomX = UnityEngine.Random.Range(-_entity.HomeRadius, _entity.HomeRadius + 1);
        var randomZ = UnityEngine.Random.Range(-_entity.HomeRadius, _entity.HomeRadius + 1);
#if PatrolDebug
        Debug.Log("X: " + randomX);
        Debug.Log("Y: " + randomX);
#endif
        _newDestination = new Vector3(_entity.InitialPosition.x + randomX, _entity.InitialPosition.y,
            _entity.InitialPosition.z + randomZ);

#if PatrolDebug
        Debug.Log("New destination is: " + _newDestination);
#endif
        _navMeshAgent.destination = _newDestination;
        _animator.SetBool("Running", true);
        _patrolling = true;
    }
}