using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        playerHealth.Shielded = true;
        print("Player is shielded");
    }
    private void OnDisable()
    {
        playerHealth.Shielded = false;
        print("Player is not shielded");
    }
}
