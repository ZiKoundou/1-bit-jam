using UnityEngine;
using UnityEngine.AI;
public class Knockback : MonoBehaviour
{
    private EnemyAi enemyAi;
    private PlayerMovement playerMovement;
    private Health health;
    private Rigidbody2D rb;

    void Awake()
    {
        enemyAi = GetComponent<EnemyAi>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    public void ApplyKnockback(Vector3 source, float force)
    {
        if(health.isInvincible) return;
        Vector2 direction = (transform.position - source).normalized;
        if (enemyAi) enemyAi.stop = true;
        rb.AddForce(force * direction, ForceMode2D.Impulse);
        if (enemyAi) enemyAi.stop = false;
    }

    
}