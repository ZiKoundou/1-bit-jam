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
    [SerializeField] GameObject FloatingTextPrefab;
    private SpriteRenderer sprite;
    public bool isInvincible { get; private set; } = false;
    #region Events
    public event Action OnDamaged;
    public event Action OnHealed;
    public event Action OnDeath;
    #endregion
    private void Awake()
    {
        currentHealth = maxHealth;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    #region Methods
    
    public bool TakeDamage(float amount)
    {
        if(WorldTimer.Instance.IsGameOver == true) return false;
        if (isInvincible) return false;
        if(amount <= 0) return false;
        currentHealth = Mathf.Max(currentHealth - amount, 0f);
        ShowFloatingText(amount);
        OnDamaged?.Invoke();

        if (currentHealth <= 0f){
            Die();
            return true;
        }
        StartCoroutine(Invincibility());
        return false;
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        // Optional: visual feedback (very helpful for debugging)
        
        if (sprite != null)
        {
            float flashInterval = 0.1f;
            float timer = 0f;
            while (timer < iFrameDuration)
            {
                sprite.enabled = !sprite.enabled;
                yield return new WaitForSeconds(flashInterval);
                timer += flashInterval;
            }
            sprite.enabled = true; // make sure it's visible at end
        }
        else
        {
            yield return new WaitForSeconds(iFrameDuration);
        }
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
    }
    #endregion

    public void SetInvincible(bool condition)
    {
        isInvincible = condition;
    }

    void ShowFloatingText(float damageAmount){

        var go = Instantiate(FloatingTextPrefab,transform.position,Quaternion.identity);
        go.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damageAmount.ToString();
    }
}