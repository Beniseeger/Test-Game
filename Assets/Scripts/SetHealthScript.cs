using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealthScript : MonoBehaviour
{
    public HealthBarScript healthBar;

    public float maxHealth = 100.0f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
