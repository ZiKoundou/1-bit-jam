using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class WorldTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float baseAdvanceRate = 0.1f;  // "Seconds" per real sec
    [SerializeField] private float startingTimeMinutes = 540f;
    [SerializeField] private float goalTimeMinutes = 480f;   // 8 hours = 480 min (9AM-5PM)
    private float timeProgress = 0f;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image progressBar;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float timeAddDuration = 1;
    [SerializeField] private float timeAddScaleUp = 0.2f;
    [Header("Win and Lose state")]
    [SerializeField] private UI winScreen;
    [SerializeField] private UI loseScreen;

    private Coroutine currentScaleRoutine;           // ← NEW: track active animation
    private Vector3 originalScale = Vector3.one;     // Cache starting scale

    [Header("Time Add Animation")]
    [SerializeField] private float maxScale = 1.6f;         // ← never bigger than this (e.g. 1.6 = 60% bigger)
    [SerializeField] private float scalePerSecond = 0.15f;  // how much extra scale per game-second added
    [SerializeField] private float animDuration = 0.4f;     // how long the pop lasts
    [SerializeField] private AnimationCurve scaleCurve;     // optional: ease in/out (recommended)
    public static WorldTimer Instance;

    public bool IsGameOver { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        timeProgress = 0f;
        originalScale = rectTransform.localScale;   // usually (1,1,1)
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGameOver == false)
        {
            timeProgress += baseAdvanceRate * Time.deltaTime;
        }
        
        UpdateUI();

        if (timeProgress >= goalTimeMinutes)
        {
            WinGame();
        }

    }

    void UpdateUI()
    {
        float displayTimeMinutes = startingTimeMinutes + timeProgress;
        // Display time: 12-hour format
        int totalHours = (int)(displayTimeMinutes / 60);
        int displayHours = totalHours % 12;
        if (displayHours == 0) displayHours = 12;
        int displayMins = (int)(displayTimeMinutes % 60);
        string ampm = totalHours >= 12 ? "PM" : "AM";
        timeText.text = $"{displayHours}:{displayMins:00} {ampm}";

        // Progress bar
        progressBar.fillAmount = Mathf.Clamp01(timeProgress / goalTimeMinutes);
    }
    public void AdvanceTimer(float bonusSeconds)
    {
        timeProgress += bonusSeconds / 60f;
        // Stop any running animation
        if (currentScaleRoutine != null)
        {
            StopCoroutine(currentScaleRoutine);
        }

        // Start new one
        currentScaleRoutine = StartCoroutine(TimeAddAnimation(bonusSeconds));
    }

    IEnumerator TimeAddAnimation(float bonusSeconds)
    {
        float extraScale = bonusSeconds/75 * scalePerSecond;           // bigger bonus = bigger pop
        float targetScale = Mathf.Min(1f + extraScale, maxScale);  // clamp to max

        float elapsed = 0f;

        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animDuration;

            // Optional: use curve for nicer easing (set in inspector: 0→1→0 or bounce)
            float curveValue = scaleCurve != null ? scaleCurve.Evaluate(t) : t;

            float currentExtra = Mathf.Lerp(0f, targetScale - 1f, curveValue);
            rectTransform.localScale = originalScale * (1f + currentExtra);

            yield return null;
        }

        // Smoothly return to normal
        elapsed = 0f;
        Vector3 startScale = rectTransform.localScale;

        while (elapsed < animDuration * 0.7f) // slightly faster return
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (animDuration * 0.7f);
            float curveValue = scaleCurve != null ? scaleCurve.Evaluate(1f - t) : (1f - t);

            rectTransform.localScale = Vector3.Lerp(startScale, originalScale, curveValue);
            yield return null;
        }

        rectTransform.localScale = originalScale;
        currentScaleRoutine = null;
    }
    
    
    void WinGame() { 
        /* Escape anim, victory screen */ IsGameOver = true; 
        winScreen.OpenWindow(winScreen.gameObject);
        
    }
    public void LoseGame() { 
        /* Escape anim, victory screen */ IsGameOver = true; 
        loseScreen.OpenWindow(loseScreen.gameObject);
    }

}
