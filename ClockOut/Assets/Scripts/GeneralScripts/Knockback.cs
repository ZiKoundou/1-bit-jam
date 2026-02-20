using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Knockback : MonoBehaviour
{
    private EnemyAi enemyAi;
    private Rigidbody2D rb;
    public float duration = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        enemyAi = GetComponent<EnemyAi>();
    }

    public void ApplyKnockback(Vector3 source, float force)
    {
        Vector2 direction = (transform.position - source).normalized;
        StartCoroutine(KnockbackCoroutine(direction, force));
    }

    IEnumerator KnockbackCoroutine(Vector2 direction, float force)
    {
        if(enemyAi)enemyAi.stop = true;
        

        rb.linearVelocity = Vector2.zero; // reset existing movement
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero;
        if(enemyAi)enemyAi.stop = false;
    }
}
