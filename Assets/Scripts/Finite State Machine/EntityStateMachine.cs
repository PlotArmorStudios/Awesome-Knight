//#define DEBUG_LOG

using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Player _player;
    private Entity _entity;

    private Idle _idle;
    private ChasePlayer _chasePlayer;
    private Attack _attack;
    private Dead _dead;
    private ReturnHome _returnHome;
    private Patrol _patrol;

    public IState CurrentState => _stateMachine.CurrentState;
    public bool IsHome => Vector3.Distance(_entity.transform.position, _entity.InitialPosition) < _entity.HomeRadius;
    private float DistanceToPlayer => Vector3.Distance(_navMeshAgent.transform.position, _player.transform.position);
    public bool Patrolling => _idle.TogglePatrol();

    private void Start()
    {
        _entity = GetComponent<Entity>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();

        _stateMachine = new StateMachine();

        InitializeStates();

        AddStateTransitions();

        //Set default state
        _stateMachine.SetState(_idle);
    }


    private void InitializeStates()
    {
        _idle = new Idle(_entity);
        _chasePlayer = new ChasePlayer(_entity, _player, _navMeshAgent);
        _attack = new Attack(_entity, _player);
        _dead = new Dead(_entity);
        _returnHome = new ReturnHome(_entity);
        _patrol = new Patrol(_entity);
    }

    private void AddStateTransitions()
    {
        _stateMachine.AddTransition(
            _idle,
            _patrol,
            () => Patrolling);

        _stateMachine.AddTransition(
            _idle,
            _chasePlayer,
            () => DistanceToPlayer < _entity.DetectionRadius);

        _stateMachine.AddTransition(
            _chasePlayer,
            _idle,
            () => DistanceToPlayer > _entity.DetectionRadius);
     
        _stateMachine.AddTransition(
            _patrol,
            _chasePlayer,
            () => DistanceToPlayer < _entity.DetectionRadius);

        _stateMachine.AddTransition(
            _chasePlayer,
            _attack,
            () => DistanceToPlayer <= _entity.AttackRadius);

        _stateMachine.AddTransition(
            _attack,
            _chasePlayer,
            () => DistanceToPlayer > _entity.AttackRadius);
        
        _stateMachine.AddTransition(
            _idle,
            _returnHome,
            () => !IsHome && _idle.UpdateReturnHomeTime());
        
        _stateMachine.AddTransition(
            _returnHome,
            _idle,
            () => IsHome);

        _stateMachine.AddTransition(
            _returnHome,
            _chasePlayer,
            () => DistanceToPlayer < _entity.DetectionRadius);

        _stateMachine.AddAnyTransition(_dead, () => _entity.Health.CurrentHealthValue <= 0);
    }

    private void Update()
    {
#if DEBUG_LOG
        Debug.Log("Enemy is patrolling: " + Patrolling);
#endif
        _stateMachine.Tick();
    }
}