using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnemyMusic : MonoBehaviour
{
    BackgroundTransition transition;

    private void Awake()
    {
        transition = GameObject.FindGameObjectWithTag("AudioHandler").GetComponent<BackgroundTransition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            print("triggered");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            print("not triggered");
        }
    }
}
