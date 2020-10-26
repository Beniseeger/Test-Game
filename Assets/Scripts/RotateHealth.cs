using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealth : MonoBehaviour
{
    public int rotateSpeed;

    public ParticleSystem particles;

    public AudioSource healSound;

    private bool triggerAlreadyEntered = false;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerTarget health = other.gameObject.GetComponent<PlayerTarget>();

        if (health != null)
        {
            if (!triggerAlreadyEntered)
            {
                if (health.GainHealth(10))
                {
                
                    triggerAlreadyEntered = true;
                    healSound.Play();
                    particles.Play();
                    gameObject.GetComponentInChildren<Renderer>().enabled = false;
                    Invoke("Die", 0.4f);
                }
                
                
            }

        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
