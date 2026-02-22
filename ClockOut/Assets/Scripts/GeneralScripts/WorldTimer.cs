using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    public static WorldTimer Instance;
    private float currentTimeMinutes;
    public bool IsGameOver { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        timeProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeProgress += baseAdvanceRate * Time.deltaTime;
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

    void WinGame() { /* Escape anim, victory screen */ IsGameOver = true; }
}
