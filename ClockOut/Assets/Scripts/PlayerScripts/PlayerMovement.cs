using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    private Rigidbody2D rb;
    [Header("Movement Variables")]
    [SerializeField] float speed = 5;
    private Vector2 movement;
    Vector3 localScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(movement.x < 0)
        {
            FlipLeft();
        }
        else if (movement.x > 0)
        {
            FlipRight();
        }
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }

    void FlipLeft()
    {
        localScale = transform.localScale;
        localScale.x = -1f; // Multiplies the x-scale by -1, effectively flipping it
        transform.localScale = localScale;
    }
    
    void FlipRight()
    {
        localScale = transform.localScale;
        localScale.x = 1f; // Multiplies the x-scale by -1, effectively flipping it
        transform.localScale = localScale;
    }
    

}
