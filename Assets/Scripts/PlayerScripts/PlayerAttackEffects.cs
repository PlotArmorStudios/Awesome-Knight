using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{
    [SerializeField] GameObject groundImpact_Spawn, kickFx_Spawn, fireTornado_Spawn, fireShield_Spawn;
    [SerializeField] GameObject groundImpact_Prefab, kickFx_Prefab, fireTornado_Prefab, fireShield_Prefab, healFX_Prefab, thunderFX_Prefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip fireImpact, fireCloak, kickArmor, heal, lightningStrike;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void GroundImpact()
    {
        Instantiate(groundImpact_Prefab, groundImpact_Spawn.transform.position, Quaternion.identity);
    }

    void ImpactSound()
    {
        PlaySound(fireImpact);
    }

    void Kick()
    {
        Instantiate(kickFx_Prefab, kickFx_Spawn.transform.position, Quaternion.identity);
    }

    void PlayKickSound()
    {
        PlaySound(kickArmor);
    }

    void FireTornado()
    {
        Instantiate(fireTornado_Prefab, fireTornado_Spawn.transform.position, Quaternion.identity);
    }

    void FireShield()
    {
        GameObject fireObj = Instantiate(fireShield_Prefab, fireShield_Spawn.transform.position, Quaternion.identity) as GameObject;
        fireObj.transform.SetParent(transform); //to position the fireObj under the parent gameobject which is the player
        PlaySound(fireCloak);
    }

    void Heal()
    {
        Vector3 temp = transform.position;
        temp.y += 2f;

        GameObject healObj = Instantiate(healFX_Prefab, temp, Quaternion.identity) as GameObject;
        healObj.transform.SetParent(transform);
    }

    void PlayHealSound()
    {
        PlaySound(heal);
    }

    void LightningSound()
    {
        PlaySound(lightningStrike);
    }

    void ThunderAttack()
    {
        for(int i = 0; i < 8; i++)
        {
            Vector3 pos = Vector3.zero;

            if(i == 0)
            {
                pos = new Vector3(transform.position.x - 4f, transform.position.y, transform.position.z);
            }
            else if (i == 1)
            {
                pos = new Vector3(transform.position.x + 4f, transform.position.y, transform.position.z);
            }
            else if (i == 2)
            {
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f);
            }
            else if (i == 3)
            {
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 4f);
            }
            else if (i == 4)
            {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z + 2.5f);
            }
            else if (i == 5)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z + 2.5f);
            }
            else if (i == 6)
            {
                pos = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z - 2.5f);
            }
            else if (i == 7)
            {
                pos = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z + 2.5f);
            }

            Instantiate(thunderFX_Prefab, pos, Quaternion.identity);
        }
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
