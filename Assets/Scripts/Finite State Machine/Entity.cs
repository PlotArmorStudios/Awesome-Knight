﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float _detectionRadius;
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected float _attackDelay;
    [SerializeField] protected float _returnHomeTime;
    [SerializeField] protected float _homeRadius;
    public Rigidbody Rigidbody { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public Player PlayerTarget { get; private set; }
    public Animator Animator { get; private set; }
    public Vector3 InitialPosition { get; private set; }
    public EntityStateMachine StateMachine { get; private set; }
    public EnemyHealth Health { get; private set; }
    
    //Public access to AI values
    public float HomeRadius { get; set; }
    public float ReturnHomeTime { get; set; }
    public float AttackDelay { get; set; }
    public float DetectionRadius { get; set; }
    public float AttackRadius { get; set; }

    private float _attackTimer;
    private bool _canResetNavMesh;

    private void Awake()
    {
        PlayerTarget = FindObjectOfType<Player>();
        NavAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        StateMachine = GetComponent<EntityStateMachine>();
        Rigidbody = GetComponent<Rigidbody>();
        InitialPosition = transform.position;
        Health = GetComponent<EnemyHealth>();
    }
}