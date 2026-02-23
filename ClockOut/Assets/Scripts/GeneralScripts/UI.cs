using UnityEngine;
public class UI : MonoBehaviour
{
    public GameObject TutorialWindow;
    public void CloseWindow(GameObject window)
    {
        // AudioManager.instance.PlaySFX("BACKCLICK");
        CanvasGroup canvasGroup = window.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OpenWindow(GameObject window)
    {
        // AudioManager.instance.PlaySFX("CLICK");
        CanvasGroup canvasGroup = window.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}