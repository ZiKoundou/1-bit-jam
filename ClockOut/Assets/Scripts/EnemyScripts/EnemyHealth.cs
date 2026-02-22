using UnityEngine;
using System;
public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Health health;
    [SerializeField] GameObject deathParticles;
    [SerializeField] GameObject hitParticles;
    [SerializeField] private Transform hitPoint;
    public static event Action OnEnemyDied;
    public float timerBonus = 100;
    void OnEnable()
    {
        health = GetComponentInParent<Health>();
        health.OnDeath += Die;
        health.OnDamaged += HandleDamaged;
    }
    void OnDisable()
    {
        health.OnDeath -= Die;
        health.OnDamaged -= HandleDamaged;

    }
    public void HandleDamaged()
    {
        SpawnHitEffect();
    }
    void SpawnHitEffect()
    {
        GameObject effect = Instantiate(hitParticles, hitPoint.position, Quaternion.identity);

        // effect.transform.rotation = Quaternion.LookRotation(Vector3.forward, hitDirection);
    }
    public void Die()
    {
        // do death stuff...
        OnEnemyDied?.Invoke();
        Instantiate(deathParticles, gameObject.transform.position, Quaternion.identity);
        WorldTimer.Instance.AdvanceTimer(timerBonus);
        Destroy(gameObject);
    }
}
