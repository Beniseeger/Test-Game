using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float damage = 5.0f;
    public float lifeTime = 0.5f;

    Transform target;
    NavMeshAgent agent;

    public ParticleSystem destroyAnim;
    public AudioSource explodeSound;
    public AudioSource damageSound;

    public float timeToNextDamage = 0.3f;
    private float timeSinceLastDamage = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }


        if (timeSinceLastDamage > 0)
        {
            timeSinceLastDamage -= Time.deltaTime;
        }
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && timeSinceLastDamage <= 0.0)
        {
            timeSinceLastDamage = timeToNextDamage;
            PlayerTarget health = other.gameObject.GetComponent<PlayerTarget>();
            damageSound.Play();
            health.TakeDamage(damage);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void DestroyGameObject()
    {
        destroyAnim.Play();
        explodeSound.Play();

        StartCoroutine(DestroyParticlesAfterLifeTime(gameObject, lifeTime));
    }

    private IEnumerator DestroyParticlesAfterLifeTime(GameObject gameObject, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
