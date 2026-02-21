using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockbackForce = 10f;
    [HideInInspector] public BoxCollider2D boxCollider;
    [SerializeField] private bool isSecondaryAttack = false;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        var health = other.GetComponent<Health>();
        var knockback = other.GetComponent<Knockback>();
        if (health == null || knockback == null) return;
        
        bool enemyDied = health.TakeDamage(damage);  // ← Returns TRUE if died
        knockback.ApplyKnockback(transform.position, knockbackForce);  // or use player transform if needed
        
        if (enemyDied && isSecondaryAttack)
        {
            var playerHitbox = FindFirstObjectByType<PlayerHitbox>();
            playerHitbox?.ResetSecondaryCooldown();
            Debug.Log("Secondary cooldown RESET on kill!");  // Optional feedback
        }
    }

}