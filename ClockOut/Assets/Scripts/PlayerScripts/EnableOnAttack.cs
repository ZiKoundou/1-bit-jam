using System.Collections;
using UnityEngine;

public class AttackEffects : MonoBehaviour
{
    [SerializeField] private PlayerHitbox playerAttack;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float duration = 0.10f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void OnEnable()
    {
        playerAttack.OnAttack += DoPunchEffect;
    }

    private void OnDisable()
    {
        playerAttack.OnAttack -= DoPunchEffect; // Always unsubscribe!
    }

    private void DoPunchEffect()
    {
        StartCoroutine(ShowAttack());
    }
    IEnumerator ShowAttack()
    {
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(duration);
        spriteRenderer.enabled = false;
    }
}