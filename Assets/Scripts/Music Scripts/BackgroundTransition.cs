using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTransition : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip mainMusic, battleMusic;

    [SerializeField] LayerMask enemyLayer;
    EnemyControl _enemyControl;
    Transform playerTarget;

    public float radius;


    public static bool enemyIsAttacking = false;
    public bool enemyIsNotAttacking = true;

    public int switchStates = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] enemiesInPlayerRange = Physics.OverlapSphere(playerTarget.transform.position, radius, enemyLayer);

        foreach(Collider c in enemiesInPlayerRange)
        {
            _enemyControl = c.gameObject.GetComponent<EnemyControl>();

            if (_enemyControl.enemyCurrentState == EnemyState.Pause || _enemyControl.enemyCurrentState == EnemyState.Attack)
            {
                enemyIsAttacking = true;
            }
            else if (_enemyControl.enemyCurrentState == EnemyState.GoBack || _enemyControl.enemyCurrentState == EnemyState.Idle || _enemyControl.enemyCurrentState == EnemyState.Walk)
            {
                enemyIsAttacking = false;
            }

            if (_enemyControl.enemyCurrentState == EnemyState.Death)
            {
                enemyIsAttacking = false;
            }
        }

        SwitchBetweenBackgroundSongs();

    }

    void SwitchBetweenBackgroundSongs()
    {
        if (enemyIsAttacking == true)
        {
            if (switchStates <= 1) //will not add if past 1
            {
                switchStates++; //add 1 to 0 (as result of leaving attack state)
            }

            if (switchStates == 1)
            {
                audioSource.volume = .35f;
                ChangeMusic(battleMusic);
                switchStates = 2; //switch to 2 so you leave the loop and also make it possible to change to main music
            }
        }
        else if (enemyIsAttacking == false)
        {
            if (switchStates == 2)
            {
                audioSource.volume = .8f;
                ChangeMusic(mainMusic);
                switchStates = 0; //leave loop and make it possible to change to battle music
            }
        }
    }

    void ChangeMusic(AudioClip music)
    {
        audioSource.Stop();

        audioSource.clip = music;

        audioSource.Play();
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.2f);
        audioSource.Stop();
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(.2f);
        audioSource.Play();
    }
}
