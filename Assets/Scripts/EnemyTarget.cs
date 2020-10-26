using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public HealthBarScript healthBar;
    private float health;
    public float maxHealth = 50.0f;
    private Score score;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            gameObject.transform.Find("Cube").Find("Cube").GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            score = GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<Score>();
            score.scoreNumber = score.scoreNumber + 1;
            gameObject.GetComponent<EnemyController>().DestroyGameObject();
            //Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
