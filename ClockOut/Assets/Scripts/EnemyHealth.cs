using UnityEngine;
using System;
public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Health health;
    [SerializeField] GameObject deathParticles;
    public static event Action OnEnemyDied;
    void OnEnable()
    {
        health = GetComponentInParent<Health>();
        health.OnDeath += Die;
    }
    void OnDisable()
    {
        health.OnDeath -= Die;
    }
    public void Die()
    {
        // do death stuff...
        OnEnemyDied?.Invoke();
        Instantiate(deathParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
