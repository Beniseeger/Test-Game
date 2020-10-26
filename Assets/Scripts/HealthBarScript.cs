using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public bool GainHealth(float health)
    {
        float diff = slider.value - health;
        if (diff >= 30)
        {
            return false;
        }

        slider.value = slider.value + health;

        

        fill.color = gradient.Evaluate(slider.normalizedValue);

        return true;
    }
}
