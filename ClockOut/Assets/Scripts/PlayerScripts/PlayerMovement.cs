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
    [SerializeField] float dashForce = 3f;
    private float currentSpeed;
    private Vector2 movement;
    private Vector2 lastMoveDir;
    [SerializeField]private PlayerHitbox playerAttack;
    [SerializeField] private float attackMoveDuration = 0.10f;
    [SerializeField] private float secondaryMoveDuration = 0.10f;
    private Coroutine primarySpeedRoutine;
    private Coroutine secondarySpeedRoutine;
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
        playerAttack.OnSecondary += SecondarySpeed;
    }

    private void OnDisable()
    {
        playerAttack.OnAttack -= AttackSpeed; // Always unsubscribe!
        playerAttack.OnSecondary += SecondarySpeed;
    }
    public void Move(InputAction.CallbackContext context)
    {
        
        movement = context.ReadValue<Vector2>();
        if(movement != Vector2.zero)
        {
            lastMoveDir = movement;
        }
        
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        rb.AddForce(lastMoveDir * dashForce, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(WorldTimer.Instance.IsGameOver == true) return;
        if(!canMove) return;
        rb.AddForce(movement * currentSpeed,ForceMode2D.Force);
    }
    private void AttackSpeed()
    {
        if (primarySpeedRoutine != null) StopCoroutine(primarySpeedRoutine);
        primarySpeedRoutine = StartCoroutine(ChangeInSpeed(attackMoveDuration));
    }
    private void SecondarySpeed()
    {
        if (secondarySpeedRoutine != null) StopCoroutine(secondarySpeedRoutine);
        secondarySpeedRoutine = StartCoroutine(ChangeInSpeed(attackMoveDuration));
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
