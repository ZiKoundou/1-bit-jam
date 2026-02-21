using UnityEngine;
using UnityEngine.UI;

public class SecondaryCooldownUI : MonoBehaviour
{
    [SerializeField] private PlayerHitbox playerHitbox;      // Drag your PlayerHitbox here
    [SerializeField] private Image radialFillImage;          // Drag the overlay Image here

    [SerializeField] private CanvasGroup canvasGroup;
    void Update()
    {
        float progress = playerHitbox.GetSecondaryCooldownProgress();  // 0 = ready, 1 = full cooldown

        radialFillImage.fillAmount = progress;  // ← magic line
        if(progress > 0.96) {
            canvasGroup.alpha = 0;
        }
        else
        {
            canvasGroup.alpha = 1;
        }
        // Optional nice touches:
        // radialFillImage.color = progress > 0 ? Color.gray : Color.white;  // dim when on CD
        // iconImage.color = progress > 0 ? new Color(0.6f,0.6f,0.6f) : Color.white;
    }

}