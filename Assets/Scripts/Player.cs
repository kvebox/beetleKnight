using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxHealth = 25f;
    public float currentHealth;
    public HealthBar healthBar;

    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update() {
        if (Input.GetButtonDown("Dash")) {
            TakeDamage(5);
        }
    }

    void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}