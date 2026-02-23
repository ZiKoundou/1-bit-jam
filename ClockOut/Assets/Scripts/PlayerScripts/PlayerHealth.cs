using UnityEngine;
using System;
public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Health health;
    [SerializeField] GameObject deathParticles;
    [SerializeField] GameObject hitParticles;
    [SerializeField] private Transform hitPoint;
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
    }
    public void Die()
    {
        Instantiate(deathParticles, gameObject.transform.position, Quaternion.identity);
        WorldTimer.Instance.LoseGame();
    }
}
