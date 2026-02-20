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
    [SerializeField] private float duration = 0.10f;
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
    }

    private void OnDisable()
    {
        playerAttack.OnAttack -= AttackSpeed; // Always unsubscribe!
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
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
    private void AttackSpeed()
    {
        StartCoroutine(ChangeInSpeed(duration));
    }

    IEnumerator ChangeInSpeed(float duration)
    {
        currentSpeed = attackMoveSpeed;   
        yield return new WaitForSeconds(duration);
        currentSpeed = baseSpeed;
    }


}
