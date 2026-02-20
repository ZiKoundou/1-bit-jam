using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Slider slider; 
    Health health;
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    void OnEnable()
    {
        health = GetComponentInParent<Health>();
        health.OnDamaged += HandleDamaged;
    }
    void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
    }
    public void SetHealth(float health, float maxHealth)
    {
        Mathf.Clamp01(slider.value = health / maxHealth);
    }

    public void HandleDamaged()
    {
        SetHealth(health.currentHealth, health.maxHealth);
    }
}
