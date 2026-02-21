using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerHitbox : MonoBehaviour
{
    
    [SerializeField] private BoxCollider2D hitboxCollider;
    [SerializeField] private float hitboxDuration;
    public float damageAmount = 1f;
    public float knockbackForce = 10f;
    public event Action OnAttack;
    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed)return;
        OnAttack?.Invoke();
        StartCoroutine(Meleee());

    }

    IEnumerator Meleee()
    {
        hitboxCollider.enabled = true;
        yield return new WaitForSeconds(hitboxDuration);
        hitboxCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(damageAmount);
            collision.GetComponent<Knockback>().ApplyKnockback(transform.position, knockbackForce);
        }
    }
}
