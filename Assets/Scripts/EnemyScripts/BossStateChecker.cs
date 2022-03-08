using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_State
{
    None,
    Idle,
    Pause,
    Attack,
    Death
}
public class BossStateChecker : MonoBehaviour
{
    Transform playerTarget;
    [SerializeField] Boss_State bossState = Boss_State.None;
    float distanceToTarget;

    EnemyHealth bossHealth;

    // Start is called before the first frame update
    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent < EnemyHealth >();
    }

    // Update is called once per frame
    void Update()
    {
        SetState();
    }

    void SetState()
    {
        distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

        if(bossState != Boss_State.Death)
        {
            if(distanceToTarget > 3f && distanceToTarget <= 9f)
            {
                bossState = Boss_State.Pause;
            }
            else if (distanceToTarget > 15f)
            {
                bossState = Boss_State.Idle;
            }
            else if(distanceToTarget <= 3f)
            {
                bossState = Boss_State.Attack;
            }
            else
            {
                bossState = Boss_State.None;
            }
            // if(bossHealth._health <= 0f)
            // {
            //     bossState = Boss_State.Death;
            // }
        }
    }

    //create an accessesor

    public Boss_State BossState
    {
        get { return bossState; }
        set { bossState = value; }
    }
}
