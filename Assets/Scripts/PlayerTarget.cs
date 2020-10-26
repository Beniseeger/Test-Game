using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public HealthBarScript healthBar;
    public GameObject gameController;
    private GameController controller;
    public AudioSource deathAudio;

    public ParticleSystem deathAnimation;

    public GameObject cam;
    public GameObject deathPanel;
    private Animator anim;
    private Animator deathPanelAnim;
    private float health;
    public float maxHealth = 50.0f;

    private bool alreadyDead = false;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = cam.GetComponent<Animator>();
        deathPanelAnim = deathPanel.GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.SetHealth(health);

        if (health <= 0 && !alreadyDead)
        {

            //Die();
            Time.timeScale = 0.5f;
            deathAnimation.Play();
            deathAudio.Play();

            Renderer[] rs = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                if(!r.gameObject.CompareTag("ParticleSystem"))
                    r.enabled = false;
            }

                


           //gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            anim.SetBool("diedFromEnemy", true);
            deathPanelAnim.SetBool("died", true);
            Invoke("Die", 0.8f);
            alreadyDead = true;
        }
    }


    public bool GainHealth(float plusHealth)
    {
        if (healthBar.GainHealth(plusHealth))
        {
            print(health);
            health = health + plusHealth;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            return true;
        }
        return false;
        
    }

    private void Die()
    {
        alreadyDead = false;
        Time.timeScale = 1f;
        anim.SetBool("diedFromEnemy", false);
        deathPanelAnim.SetBool("died", false);
        controller = gameController.GetComponent<GameController>();
        controller.Restart();
    }
}