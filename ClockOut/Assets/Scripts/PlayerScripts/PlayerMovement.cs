using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    private Rigidbody2D rb;
    [Header("Movement Variables")]
    [SerializeField] float baseSpeed = 5;
    [SerializeField] float attackMoveSpeed = 2.5f;
    private float currentSpeed;
    private Vector2 movement;
    [SerializeField]private PlayerHitbox playerAttack;
    [SerializeField] private float attackMoveDuration = 0.10f;
    [SerializeField] private float secondaryMoveDuration = 0.10f;
    
    private bool canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // playerAttack = GetComponent<PlayerHitbox>();
        currentSpeed = baseSpeed;
    }
    private void OnEnable()
    {
        playerAttack.OnAttack += AttackSpeed;
        playerAttack.OnSecondary += AttackSpeed;
    }

    private void OnDisable()
    {
        playerAttack.OnAttack -= AttackSpeed; // Always unsubscribe!
        playerAttack.OnSecondary += AttackSpeed;
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(!canMove) return;
        rb.AddForce(movement * currentSpeed,ForceMode2D.Force);
    }
    private void AttackSpeed()
    {
        StartCoroutine(ChangeInSpeed(attackMoveDuration));
    }
    private void SecondarySpeed()
    {
        StartCoroutine(ChangeInSpeed(secondaryMoveDuration));
    }


    IEnumerator ChangeInSpeed(float duration)
    {
        currentSpeed = attackMoveSpeed;   
        yield return new WaitForSeconds(duration);
        currentSpeed = baseSpeed;
    }
    public void EnableMovement()
    {
        canMove = true;
    }

       public void DisableMovement()
    {
        canMove = false;
    }

}
