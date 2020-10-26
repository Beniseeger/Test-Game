using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public ParticleSystem qAbility;

    public float qCooldown = 5f;
    private float qSinceLastPress = 0;

    // Start is called before the first frame update
    void Start()
    {
        qSinceLastPress = qCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && qSinceLastPress > qCooldown)
        {
            qAbility.Play();

            qSinceLastPress = 0;
        }

        qSinceLastPress += Time.deltaTime;
    }
}
