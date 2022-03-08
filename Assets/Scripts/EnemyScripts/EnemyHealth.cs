using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _MaxHealth = 100f;
    private float _health;
    public Image healthImg;
    public float CurrentHealthValue => _health;

    private void Start()
    {
        _health = _MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        // Convert health whole number value into a decimal value that is less than 1
        healthImg.fillAmount = _health / 100f;

        print("Enemy took damage, health is " + _health);
    }
}