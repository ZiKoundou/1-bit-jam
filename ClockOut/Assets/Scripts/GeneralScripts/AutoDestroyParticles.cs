using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField] private float checkDelay = 0.1f;  // How often to check (perf-friendly)

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps == null)
        {
            Debug.LogError("No ParticleSystem found! Destroying self.", this);
            Destroy(gameObject);
            return;
        }

        // Start checking after emission stops (Duration + buffer)
        InvokeRepeating(nameof(CheckAndDestroy), ps.main.duration + ps.main.startLifetime.constantMax * 0.5f, checkDelay);
    }

    void CheckAndDestroy()
    {
        if (ps == null || ps.IsAlive(false)) return;  // Still alive? Wait...

        // All dead → destroy
        Destroy(gameObject);
    }
}