using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float knockbackForce = 8f;   // optional

    private Collider2D hitCollider;  // cache it

    void Awake()
    {
        hitCollider = GetComponent<Collider2D>();
        if (hitCollider == null)
        {
            Debug.LogError("No Collider2D on this AttackHitbox child!", this);
        }

        // Start disabled – very important!
        hitCollider.enabled = false;
    }

    // Called by animation events
    public void EnableAttack()
    {
        hitCollider.enabled = true;
        // Optional: play attack sound, VFX, etc.
    }

    // Called by animation events
    public void DisableAttack()
    {
        hitCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage


            // Optional knockback
            var kb = other.GetComponent<Knockback>();
            if (kb != null)
            {
                kb.ApplyKnockback(transform.position,knockbackForce);
            }
            
            var health = other.GetComponent<Health>();  // or PlayerHealth
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            // Optional: only hit once per attack (add bool hasHitThisAttack, reset on DisableAttack)
        }
    }
}