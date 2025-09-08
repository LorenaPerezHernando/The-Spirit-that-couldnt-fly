using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMobileMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.15f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator animator;

    Rigidbody2D rb;
    float moveX;
    bool isGrounded;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (groundCheck) isGrounded =
            Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocityY);

        if (sprite && Mathf.Abs(moveX) > 0.01f) sprite.flipX = moveX < 0;
        if (animator) animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
    }

    // Llamado por PlayerInput cuando cambia "Move"
    void OnMove(InputValue v)
    {
        var m = v.Get<Vector2>();
        moveX = Mathf.Clamp(m.x, -1f, 1f);
    }

    // Llamado por PlayerInput cuando se pulsa "Jump"
    void OnJump(InputValue v)
    {
        if (v.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 0f);
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
