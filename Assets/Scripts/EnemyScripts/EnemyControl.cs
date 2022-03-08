using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    Idle,
    Walk,
    Run,
    Pause,
    GoBack,
    Attack,
    Death
}

public class EnemyControl : MonoBehaviour
{

    float attackDistance = 1.5f;
    float alertAttackDistance = 5f;
    float followDistance = 12f;

    float enemyToPlayerDistance;

    [SerializeField]
    public EnemyState enemyCurrentState = EnemyState.Idle;

    EnemyState enemyLastState = EnemyState.Idle;

    Transform playerTarget;
    Vector3 initialPosition;
    float moveSpeed = 2f;
    float walkSpeed = 1f;

    CharacterController charController;
    Vector3 whereToMove = Vector3.zero;

    float currentAttackTime;
    float waitAttackTime = 1f;

    Animator anim;
    bool finishedAnimation = true;
    bool finishedMovement = true;

    NavMeshAgent navAgent;
    Vector3 whereToNavigate;

    //health script
    EnemyHealth enemyHealth;

    //Enemy sounds
    AudioSource audioSource;
    [SerializeField] AudioClip growlSound, firstSwordSwingSound, secondSwordSwingSound;
    public int oneShotSoundInt = 0;

    // Start is called before the first frame update
    void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        initialPosition = transform.position;
        whereToNavigate = transform.position;

        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //if health is less than or equal to 0 we will set the state to death
        // if(enemyHealth._health <= 0f)
        // {
        //     enemyCurrentState = EnemyState.Death;
        // }

        if (enemyCurrentState != EnemyState.Death)
        {
            enemyCurrentState = SetEnemyState(enemyCurrentState, enemyLastState, enemyToPlayerDistance);

            if (finishedMovement)
            {
               GetStateControl(enemyCurrentState);
            }

            else
            {
                if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                   finishedMovement = true;
                }
                else if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk1") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk2"))
                {
                    anim.SetInteger("Atk", 0);
                }
            }
        }


        else
        {
            anim.SetBool("Death", true);
            charController.enabled = false;
            navAgent.enabled = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject, 2f);
            }
        }
    }

    EnemyState SetEnemyState(EnemyState curState, EnemyState lastState, float enemyToPlayerDis)
    {
        enemyToPlayerDis = Vector3.Distance(transform.position, playerTarget.position);
        float initialDistance = Vector3.Distance(initialPosition, transform.position);

        if(initialDistance > followDistance)
        {
            lastState = curState;
            curState = EnemyState.GoBack;
        }
        else if(enemyToPlayerDis <= attackDistance)
        {
            lastState = curState;
            curState = EnemyState.Attack;
        }
        else if(enemyToPlayerDis >= alertAttackDistance && lastState == EnemyState.Pause || lastState == EnemyState.Attack)
        {
            lastState = curState;
            curState = EnemyState.Pause;
        }
        else if(enemyToPlayerDis <= alertAttackDistance && enemyToPlayerDis > attackDistance)
        {
            if(curState != EnemyState.GoBack || lastState == EnemyState.Walk)
            {
                lastState = curState;
                curState = EnemyState.Pause;
            }
        }
        else if(enemyToPlayerDis > alertAttackDistance && lastState != EnemyState.GoBack && lastState != EnemyState.Pause)
        {
            lastState = curState;
            curState = EnemyState.Walk;
        }

        return curState;
    }

    void GetStateControl(EnemyState curState)
    {
        if (curState == EnemyState.Run || curState == EnemyState.Pause)
        {
            
            if (curState != EnemyState.Attack)
            {

                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
                //play growl sound here
                if (oneShotSoundInt == 0)
                {
                    PlaySound(growlSound);
                    oneShotSoundInt++;
                }
                if(Vector3.Distance(transform.position, targetPosition) >= 2.1f)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", true);

                    navAgent.SetDestination(targetPosition);
                }
            }
        }
        else if(curState == EnemyState.Attack)
        {
            anim.SetBool("Run", false);
            whereToMove.Set(0f, 0f, 0f);
            navAgent.SetDestination(transform.position); //rotate enemy toward player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTarget.position - transform.position), 5f * Time.deltaTime);

            if(currentAttackTime >= waitAttackTime)
            {
                int atkRange = Random.Range(1, 3);
                anim.SetInteger("Atk", atkRange);
                
                if(anim.GetInteger("Atk") == 1)
                {
                    //play first sword sound
                    PlaySound(firstSwordSwingSound);
                }
                else if(anim.GetInteger("Atk") == 2)
                {
                    //play second sword sound
                    PlaySound(secondSwordSwingSound);
                }
                finishedAnimation = false;
                currentAttackTime = 0f;
            }
            else
            {
                anim.SetInteger("Atk", 0);
                currentAttackTime += Time.deltaTime;
            }
        }
        else if(curState == EnemyState.GoBack)
        {
            oneShotSoundInt = 0;
            anim.SetBool("Run", true);
            Vector3 targetPosition = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);

            navAgent.SetDestination(targetPosition);

            if(Vector3.Distance(targetPosition, initialPosition) <= 3.5f)
            {
                enemyLastState = curState;
                curState = EnemyState.Walk;
            }
        }
        else if(curState == EnemyState.Walk)
        {
            oneShotSoundInt = 0;
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);

            if(Vector3.Distance(transform.position, whereToNavigate) <= 2f)
            {
                whereToNavigate.x = Random.Range(initialPosition.x - 5f, initialPosition.x + 5f);
                whereToNavigate.z = Random.Range(initialPosition.z - 5f, initialPosition.z + 5f);
            }
            else
            {
                navAgent.SetDestination(whereToNavigate);
            }
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
            whereToMove.Set(0f, 0f, 0f);
            navAgent.isStopped = true;
        }
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    IEnumerator PausePatrol()
    {
        float rand = Random.Range(2f, 6f);
        yield return new WaitForSeconds(rand);
        navAgent.SetDestination(whereToNavigate);
    }
}
