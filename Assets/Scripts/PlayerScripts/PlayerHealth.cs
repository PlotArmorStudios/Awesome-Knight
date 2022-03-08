using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    bool isShielded;

    private Image healthImg;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthImg = GameObject.Find("Health Icon").GetComponent<Image>();
    }

    public bool Shielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

    public void TakeDamage(float amount)
    {
        if (!isShielded)
        {
            health -= amount;

            healthImg.fillAmount = health / 100f;

            print("Player took damage, health is " + health);

            if(health <= 0f)
            {
                //player dies
                anim.SetBool("Death", true);

                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95)
                {
                    //player died
                    //destroy player
                    Destroy(gameObject, 2f);
                }
            }
        }
    }

    public void HealPlayer(float healAmount)
    {
        health += healAmount;

        

        if(health > 100f)
        {
            health = 100f;
        }

        healthImg.fillAmount = health / 100f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
