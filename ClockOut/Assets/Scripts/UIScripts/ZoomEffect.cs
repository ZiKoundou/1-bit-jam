using UnityEngine;

public class ZoomEffect : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Call this to flash!
    public void TriggerFlash()
    {
        animator.SetTrigger("Zoom");
    }
}