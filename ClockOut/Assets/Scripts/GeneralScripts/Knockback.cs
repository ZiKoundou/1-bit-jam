using UnityEngine;
using UnityEngine.AI;
public class Knockback : MonoBehaviour
{
    private EnemyAi enemyAi;
    private PlayerMovement playerMovement;

    [SerializeField] private float knockbackSpeed = 8f;
    [SerializeField] private float knockbackDuration = 0.25f;
    [SerializeField] private AnimationCurve knockbackFalloff; // optional: 1→0 curve for nicer easing
    private Vector2 knockbackVelocity;
    private float knockbackTimer;

    void Awake()
    {
        enemyAi = GetComponent<EnemyAi>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void ApplyKnockback(Vector3 source, float force)
    {
        Vector2 direction = (transform.position - source).normalized;
        knockbackVelocity = direction * force;   // force = e.g. 5–12 feels good
        knockbackTimer    = knockbackDuration;

        if (enemyAi) enemyAi.stop = true;
        if (playerMovement) playerMovement.DisableMovement();
    }

    void Update()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;

            float t = knockbackTimer / knockbackDuration;
            // optional: nicer easing
            float strength = knockbackFalloff != null ? knockbackFalloff.Evaluate(1-t) : t;

            transform.position += (Vector3)(knockbackVelocity * strength * Time.deltaTime);

            if (knockbackTimer <= 0)
            {
                if (enemyAi) enemyAi.stop = false;
                if (playerMovement) playerMovement.EnableMovement();
            }
        }
    }
}