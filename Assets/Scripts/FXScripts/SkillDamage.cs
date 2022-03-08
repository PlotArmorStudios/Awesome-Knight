using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour //This script creates a sphere that detects enemy collisions upon instantiation of an effect prefab, which in turn does damage
    //to the enemy upon collision
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float radius = 0.5f;
    [SerializeField] float damageCount = 10f;

    EnemyHealth enemyHealth;
    bool collided;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer); //creates a sphere at the transform of the skill's hit boxes with a radius (radius variable here}, and detects every game object on the layer of "Enemy"

        foreach(Collider collider in hits) //this code allows me to create an AOE attack
        {
            if (collider.isTrigger) //tells if collider is a trigger or not
            {
                continue; //this will simply pass through triggers on the Enemy layer
            }

            enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
            collided = true;
        }

        if (collided)
        {
            enemyHealth.TakeDamage(damageCount);
            enabled = false;
        }
    }
}
