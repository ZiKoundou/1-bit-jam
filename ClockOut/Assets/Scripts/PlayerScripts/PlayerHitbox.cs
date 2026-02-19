using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerHitbox : MonoBehaviour
{
    
    [SerializeField] private BoxCollider2D hitboxCollider;
    [SerializeField] private float hitboxDuration;
    public event Action OnAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
        StartCoroutine(Meleee());

    }

    IEnumerator Meleee()
    {
        hitboxCollider.enabled = true;
        yield return new WaitForSeconds(hitboxDuration);
        hitboxCollider.enabled = false;
    }
}
