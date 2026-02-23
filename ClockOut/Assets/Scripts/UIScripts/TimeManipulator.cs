using System.Collections;
using UnityEngine;

public class TimeManipulator : MonoBehaviour
{
    public static TimeManipulator Instance;

    [Header("Hit-Stop Settings")]
    [SerializeField] private float defaultHitStopDuration = 0.08f;  // 0.05–0.15s feels punchy

    [Header("Slow-Mo Settings")]
    [SerializeField] private float slowMoMultiplier = 0.3f;         // 30% speed
    [SerializeField] private float defaultSlowMoDuration = 0.5f;
    bool waiting;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void HitStop(float duration = -1f)
    {
        
        if (waiting)
            return;
        Time.timeScale = 0;
        
        StartCoroutine(Wait(duration < 0 ? defaultHitStopDuration : duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        waiting = false;
    }

    // 🔥 SLOW MOTION
    public void SlowMotion(float duration = -1f)
    {
        StartCoroutine(SlowMoCoroutine(duration < 0 ? defaultSlowMoDuration : duration));
    }

    IEnumerator SlowMoCoroutine(float duration)
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = slowMoMultiplier;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
    }

    // 🔥 PRE-EFFECT PAUSE (Tiny + Slow-Mo Combo)
    public void PauseThenEffect(System.Action effectCallback, float pauseDuration = 0.04f)
    {
        StartCoroutine(PauseEffectCoroutine(pauseDuration, effectCallback));
    }

    IEnumerator PauseEffectCoroutine(float pauseDuration, System.Action effect)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseDuration);
        Time.timeScale = 1f;

        effect?.Invoke();  // Trigger your particles/sound/UI pop NOW
    }
}