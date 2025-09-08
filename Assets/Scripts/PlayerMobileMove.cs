using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMobileMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.3f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator animator;

    Rigidbody2D rb;
    float moveX;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (groundCheck) 
            isGrounded =Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

        // —— mover en X con linearVelocity (Unity 6)
        var v = rb.linearVelocity;
        v.x = moveX * moveSpeed;
        rb.linearVelocity = v;

        if (sprite && Mathf.Abs(moveX) > 0.01f) sprite.flipX = moveX < 0;
        if (animator) animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
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
