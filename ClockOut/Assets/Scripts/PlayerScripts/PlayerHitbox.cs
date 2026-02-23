using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private AttackHitbox primaryHitbox;      // Primary attack hitbox
    [SerializeField] private AttackHitbox secondaryHitbox;     // Secondary attack hitbox
    [SerializeField] private float hitboxDuration;
    

    [Header("Secondary Attack Cooldown")]
    [SerializeField] private float secondaryCooldown = 1.5f;

    public event Action OnAttack;
    public event Action OnSecondary;

    private float lastSecondaryAttackTime;

    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnAttack?.Invoke();
        
        StartCoroutine(Melee(primaryHitbox.boxCollider, hitboxDuration));
    }

    public void Secondary(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Time.time < lastSecondaryAttackTime + secondaryCooldown) return;

        lastSecondaryAttackTime = Time.time;
        OnSecondary?.Invoke();
        StartCoroutine(Melee(secondaryHitbox.boxCollider, hitboxDuration));
    }

    IEnumerator Melee(BoxCollider2D collider, float duration)
    {
        
        collider.enabled = true;
        yield return new WaitForSeconds(duration);
        collider.enabled = false;
    }


    public float GetSecondaryCooldownProgress()
    {
        float elapsed = Time.time - lastSecondaryAttackTime;
        return Mathf.Clamp01(elapsed / secondaryCooldown);
    }

    // Add this to your PlayerHitbox class
    public void ResetSecondaryCooldown()
    {
        lastSecondaryAttackTime = Time.time - secondaryCooldown;  
        // or simply: lastSecondaryAttackTime = 0f;  // if you prefer always instant reset
    }

}