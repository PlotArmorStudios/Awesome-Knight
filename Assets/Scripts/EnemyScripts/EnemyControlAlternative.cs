using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyControlAlternative : MonoBehaviour
{
    [SerializeField] Transform[] walkPoints;
    int walkIndex = 0;

    Transform playerTarget;
    Animator anim;
    NavMeshAgent navAgent;

    float walkDistance = 8f;
    float attackDistance = 2f;

    float currentAttackTime;
    float waitAttackTime = 1f;

    Vector3 nextDestination;

    // Start is called before the first frame update
    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if(distance > walkDistance)
        {
            //when we have reached our destination
            if(navAgent.remainingDistance <= 0.5f) //remaining Distance is akin to calculating Vector3.Distance(transform.position, destination). Gives the distance between current position and destination
            {
                navAgent.isStopped = false;
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                anim.SetInteger("Atk", 0);

                nextDestination = walkPoints[walkIndex].position;

                navAgent.SetDestination(nextDestination);

                //so index does not become higher than array
                if(walkIndex == walkPoints.Length - 1)
                {
                    walkIndex = 0;
                }
                else
                {
                    walkIndex++;
                }
            }
        }
        else
        {
            if(distance < attackDistance)
            {
                navAgent.isStopped = false;
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
                anim.SetInteger("Atk", 0);

                navAgent.SetDestination(playerTarget.position);
;            }
            else
            {
                navAgent.isStopped = true;
                anim.SetBool("Run", false);

                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f * Time.deltaTime); //Quaternion.LookRotation gives the difference between target position and transform.position

                if(currentAttackTime >= waitAttackTime)
                {
                    int atkRange = Random.Range(1, 3);
                    anim.SetInteger("Atk", atkRange);
                    currentAttackTime = 0f;
                }
                else
                {
                    anim.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;
                }
            }
        }
    }
}
