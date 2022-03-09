using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Image m_fillWaitImage_1;
    [SerializeField] Image m_fillWaitImage_2;
    [SerializeField] Image m_fillWaitImage_3;
    [SerializeField] Image m_fillWaitImage_4;
    [SerializeField] Image m_fillWaitImage_5;
    [SerializeField] Image m_fillWaitImage_6;
    [SerializeField] private TMP_Text _moreManaText;

    int[] m_fadeImages = new int[] {0, 0, 0, 0, 0, 0};
    Animator m_animator;
    bool m_canAttack = true;

    Player _mPlayer;

    //mana
    [SerializeField] float mana = 100f;
    [SerializeField] Image m_manaImage;
    bool manaReplenish = true;

    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        _mPlayer = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Turn off the ability to attack if not standing
        if (!m_animator.IsInTransition(0) && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            m_canAttack = true;
        }
        else
        {
            m_canAttack = false;
        }

        CheckToFade();

        if (m_canAttack)
        {
            CheckInput();
        }

        m_manaImage.fillAmount = mana / 100f;

        if (mana <= 0f)
        {
            mana = 0f;
        }

        if (manaReplenish)
        {
            ReplenishMana();
        }
    }

    void ReplenishMana()
    {
        if (mana >= 100f)
        {
            mana = 100f;
            manaReplenish = false;
        }

        if (mana < 100f)
        {
            mana += Time.deltaTime * 5f;
        }
    }

    void CheckInput()
    {
        //check if we have finished our movement
        if (m_animator.GetInteger("Atk") == 0) //if not attacking, we are moving, and we have not finished movement
        {
            _mPlayer.FinishedMovement = false;

            if (!m_animator.IsInTransition(0) &&
                m_animator.GetCurrentAnimatorStateInfo(0)
                    .IsName("Stand")) //if not in transition and we are standing, we are finished moving
            {
                _mPlayer.FinishedMovement = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (mana >= 20f)
            {
                _mPlayer.TargetPosition =
                    transform.position; //set targetposition to cuurrent position of the player so player does not move

                // m_fadeImages[0] meaing image that's at index 0 e.g. the first image
                if (_mPlayer.FinishedMovement && m_fadeImages[0] != 1 && m_canAttack)
                {
                    m_fadeImages[0] = 1;
                    m_animator.SetInteger("Atk", 1);
                    mana -= 20f;
                    manaReplenish = false;
                    StartCoroutine(DelayManaReplenish());
                }
            }
            else
            {
                StopAllCoroutines();
                WarnPlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (mana >= 10f)
            {
                _mPlayer.TargetPosition =
                    transform.position; //set targetposition to cuurrent position of the player so player does not move

                // m_fadeImages[1] meaing image that's at index 0 e.g. the first image
                if (_mPlayer.FinishedMovement && m_fadeImages[1] != 1 && m_canAttack)
                {
                    m_fadeImages[1] = 1;
                    m_animator.SetInteger("Atk", 2);
                    mana -= 10f;
                    manaReplenish = false;
                    StartCoroutine(DelayManaReplenish());
                }
            }
            else
            {
                StopAllCoroutines();
                WarnPlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (mana >= 30f)
            {
                _mPlayer.TargetPosition =
                    transform.position; //set targetposition to cuurrent position of the player so player does not move

                // m_fadeImages[1] meaing image that's at index 0 e.g. the first image
                if (_mPlayer.FinishedMovement && m_fadeImages[2] != 1 && m_canAttack)
                {
                    m_fadeImages[2] = 1;
                    m_animator.SetInteger("Atk", 3);
                    mana -= 30f;
                    manaReplenish = false;
                    StartCoroutine(DelayManaReplenish());
                }
            }
            else
            {
                StopAllCoroutines();
                WarnPlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (mana >= 25f)
            {
                _mPlayer.TargetPosition =
                    transform.position; //set targetposition to cuurrent position of the player so player does not move

                // m_fadeImages[1] meaing image that's at index 0 e.g. the first image
                if (_mPlayer.FinishedMovement && m_fadeImages[3] != 1 && m_canAttack)
                {
                    m_fadeImages[3] = 1;
                    m_animator.SetInteger("Atk", 4);
                    mana -= 25f;
                    manaReplenish = false;
                    StartCoroutine(DelayManaReplenish());
                }
            }
            else
            {
                StopAllCoroutines();
                WarnPlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (mana >= 25f)
            {
                _mPlayer.TargetPosition =
                    transform.position; //set targetposition to cuurrent position of the player so player does not move

                // m_fadeImages[1] meaing image that's at index 0 e.g. the first image
                if (_mPlayer.FinishedMovement && m_fadeImages[4] != 1 && m_canAttack)
                {
                    m_fadeImages[4] = 1;
                    m_animator.SetInteger("Atk", 5);
                    mana -= 25f;
                    manaReplenish = false;
                    StartCoroutine(DelayManaReplenish());
                }
            }
            else
            {
                StopAllCoroutines();
                WarnPlayer();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (mana >= 50f)
            {
                _mPlayer.TargetPosition =
                    transform.position; //set targetposition to cuurrent position of the player so player does not move

                // m_fadeImages[1] meaing image that's at index 0 e.g. the first image
                if (_mPlayer.FinishedMovement && m_fadeImages[5] != 1 && m_canAttack)
                {
                    m_fadeImages[5] = 1;
                    m_animator.SetInteger("Atk", 6);
                    mana -= 50f;
                    manaReplenish = false;
                    StartCoroutine(DelayManaReplenish());
                }
            }
            else
            {
                StopAllCoroutines();
                WarnPlayer();
            }
        }
        else
        {
            m_animator.SetInteger("Atk", 0);
        }

        //have player rotate toward current mouseposition while pressing space
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 targetPos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(targetPos - transform.position), 15f * Time.deltaTime);
        }
    }

    private void WarnPlayer()
    {
        StartCoroutine(FlashNotice());
    }

    private IEnumerator FlashNotice()
    {
        bool flash = true;
        float alpha = 0;

        while (alpha < 1 && flash)
        {
            alpha += .75f * Time.deltaTime;
            _moreManaText.color = new Color(1, 1, 1, alpha);

            
            if (alpha >= 1)
                flash = false;
            
            yield return null;
        }

        StartCoroutine(DullNotice());
    }

    private IEnumerator DullNotice()
    {
        float alpha = 1;

        while (alpha > 0)
        {
            alpha -= .25f * Time.deltaTime;
            _moreManaText.color = new Color(1, 1, 1, alpha);

            yield return null;
        }
    }

    IEnumerator DelayManaReplenish()
    {
        yield return new WaitForSeconds(2f);
        manaReplenish = true;
    }

    void CheckToFade()
    {
        if (m_fadeImages[0] == 1)
        {
            if (FadeAndWait(m_fillWaitImage_1, 1.0f))
            {
                m_fadeImages[0] = 0;
            }
        }

        if (m_fadeImages[1] == 1)
        {
            if (FadeAndWait(m_fillWaitImage_2, .7f))
            {
                m_fadeImages[1] = 0;
            }
        }

        if (m_fadeImages[2] == 1)
        {
            if (FadeAndWait(m_fillWaitImage_3, .1f))
            {
                m_fadeImages[2] = 0;
            }
        }

        if (m_fadeImages[3] == 1)
        {
            if (FadeAndWait(m_fillWaitImage_4, .2f))
            {
                m_fadeImages[3] = 0;
            }
        }

        if (m_fadeImages[4] == 1)
        {
            if (FadeAndWait(m_fillWaitImage_5, .3f))
            {
                m_fadeImages[4] = 0;
            }
        }

        if (m_fadeImages[5] == 1)
        {
            if (FadeAndWait(m_fillWaitImage_6, .08f))
            {
                m_fadeImages[5] = 0;
            }
        }
    }

    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        bool faded = false;

        if (fadeImage == null)
        {
            return faded;
        }

        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if (fadeImage.fillAmount <= 0.0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }
}