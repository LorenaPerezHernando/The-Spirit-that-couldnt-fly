using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMobileMove : MonoBehaviour
{
    [Header("Movement")]
    //[SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float maxSpeed = 3.5f;         
    [SerializeField] private float accelerationTime = 0.1f;
    [SerializeField] private float decelerationTime = 0.1f;

    private float moveX;
    private float targetSpeed;   
    private float currentSpeed;  
    private float accel;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.3f;
    [SerializeField] private LayerMask groundMask;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (groundCheck)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

        targetSpeed = moveX * maxSpeed;

        accel = Mathf.Abs(targetSpeed) > 0.01f
            ? maxSpeed / accelerationTime  
            : maxSpeed / decelerationTime;  


        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accel * Time.deltaTime);


        var v = rb.linearVelocity;
        v.x = currentSpeed;
        rb.linearVelocity = v;

        if (sprite && Mathf.Abs(moveX) > 0.01f) sprite.flipX = moveX < 0;
        if (animator) animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
    }

    void OnMove(InputValue input)
    {
        var m = input.Get<Vector2>();
        moveX = Mathf.Clamp(m.x, -1f, 1f);
    }

    void OnJump(InputValue input)
    {
        if (input.isPressed && isGrounded)
        {
            // resetear Y y aplicar salto
            var v = rb.linearVelocity;
            v.y = 0f;
            rb.linearVelocity = v;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (animator) animator.SetTrigger("Jump");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
