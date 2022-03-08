using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornado : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float radius = 0.5f;
    [SerializeField] float damageCount = 10f;

    [SerializeField] GameObject fireExplosion;
    EnemyHealth enemyHealth;
    bool collided;

    float speed = 3f;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward); //sets rotation of fire tornado to the forward rotation of the player
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForDamage();
    }
    void Move()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer); //creates a sphere at the transform of the skill's hit boxes with a radius (radius variable here}, and detects every game object on the layer of "Enemy"

        foreach (Collider c in hits) //this code allows me to create an AOE attack
        {
            enemyHealth = c.gameObject.GetComponent<EnemyHealth>();
            collided = true;
        }

        if (collided)
        {
            enemyHealth.TakeDamage(damageCount);
            Vector3 temp = transform.position;
            temp.y += 2f;
            Instantiate(fireExplosion, temp, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void PlaySound(AudioClip clip)
    {
    }
}
