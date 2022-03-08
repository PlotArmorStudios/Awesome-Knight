using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshTest : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(new Vector3(27f, 80f, 80f));
    }
}
