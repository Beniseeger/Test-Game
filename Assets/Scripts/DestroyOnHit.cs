using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    private ParticleSystem collisionAnim;

    public float damage = 10.0f;
    private void Start()
    {
        
        collisionAnim = GetComponent<ParticleSystem>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        collisionAnim.Play();

        if (collision.gameObject.tag == "Enemy")
        {
            

            collision.gameObject.GetComponent<EnemyTarget>().TakeDamage(damage);
        }
        
        Invoke("DestroyObject", 0.05f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
