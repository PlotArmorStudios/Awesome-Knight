using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossControl : MonoBehaviour
{
    Transform playerTarget;
    BossStateChecker bossStateChecker;
    NavMeshAgent navAgent;
    Animator anim;

    bool finishedAttacking = true;
    float currentAttackTime;
    float waitAttackTime = 1f;

    //Boss growl and attack sounds
    AudioSource audioSource;
    [SerializeField] AudioClip growlSound, firstAttackSound, secondAttackSound, thirdAttackSound, fourthAttackSound;
    BackgroundTransition backgroundMusicTransitionScript;

    public int oneShotSoundInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusicTransitionScript = GameObject.Find("AudioHandler").GetComponent<BackgroundTransition>();

        audioSource = GetComponent<AudioSource>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossStateChecker = GetComponent<BossStateChecker>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedAttacking)
        {
            GetStateControl();
        }
        else
        {
            anim.SetInteger("Atk", 0);

            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finishedAttacking = true;
            }
        }
    }

    void GetStateControl()
    {
        if(bossStateChecker.BossState != Boss_State.Pause)
        {
            oneShotSoundInt = 0;
        }

        if(bossStateChecker.BossState == Boss_State.Idle || bossStateChecker.BossState == Boss_State.None)
        {
            BackgroundTransition.enemyIsAttacking = false;
        }

        if(bossStateChecker.BossState == Boss_State.Death)
        {
            BackgroundTransition.enemyIsAttacking = false;

            navAgent.isStopped = true;
            anim.SetBool("Death", true);
            Destroy(gameObject, 3f);
        }
        else
        {
            if(bossStateChecker.BossState == Boss_State.Pause)
            {
                BackgroundTransition.enemyIsAttacking = true;

                if (oneShotSoundInt == 0)
                {
                    PlaySound(growlSound);
                    oneShotSoundInt++;
                }

                navAgent.isStopped = false;
                anim.SetBool("Run", true);
                navAgent.SetDestination(playerTarget.position);
            }
            else if(bossStateChecker.BossState == Boss_State.Attack)
            {
                BackgroundTransition.enemyIsAttacking = true;
                anim.SetBool("Run", false);
                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
                //rotate towards player...............effect boss's rotation......................
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f * Time.deltaTime);

                if(currentAttackTime >= waitAttackTime)
                {
                    int atkRange = Random.Range(1, 5);
                    anim.SetInteger("Atk", atkRange);

                    if (anim.GetInteger("Atk") == 1)
                    {
                        PlaySound(firstAttackSound);
                    }
                    if (anim.GetInteger("Atk") == 2)
                    {
                        PlaySound(secondAttackSound);
                    }
                    if (anim.GetInteger("Atk") == 3)
                    {
                        PlaySound(thirdAttackSound);
                    }
                    if (anim.GetInteger("Atk") == 4)
                    {
                        PlaySound(fourthAttackSound);
                    }

                    currentAttackTime = 0f;
                    finishedAttacking = false;
                }
                else
                {
                    anim.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;
                }
            }
            else
            {
                anim.SetBool("Run", false);
                navAgent.isStopped = true;
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
