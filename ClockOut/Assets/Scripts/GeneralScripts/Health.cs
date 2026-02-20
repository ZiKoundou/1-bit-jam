using UnityEngine;
using System;
using System.ComponentModel;
using TMPro;
using System.Collections;
public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float currentHealth;
    [SerializeField] private float iFrameDuration = 0.5f;
    private bool isInvincible;
    #region Events
    public event Action OnDamaged;
    public event Action OnHealed;
    public event Action OnDeath;
    #endregion
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    #region Methods
    
    public void TakeDamage(float amount)
    {
        if (isInvincible) return;
        if(amount <= 0) return;
        currentHealth = Mathf.Max(currentHealth - amount, 0f);
        OnDamaged?.Invoke();

        if (currentHealth <= 0f){
            Die();
        }
        StartCoroutine(Invincibility());
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(iFrameDuration);
        isInvincible = false;
    }
    public void Heal(float amount)
    {
        if(amount <= 0) return;
        currentHealth = Mathf.Min(currentHealth += amount, maxHealth);
        OnHealed?.Invoke();
        
    }

    
    
    public void Die()
    {
        Debug.Log("invoking the death event");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
    #endregion

}