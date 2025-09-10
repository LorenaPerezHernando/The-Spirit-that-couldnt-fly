using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements.Experimental;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMobileMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float accelerationTimeGround = 0.1f;
    [SerializeField] private float decelerationTimeGround = 0.1f;
    [SerializeField] private float accelerationTimeAir = 0.18f; 
    [SerializeField] private float decelerationTimeAir = 0.25f;  // en aire: conserva inercia

    private float moveX;
    private float targetSpeed;
    private float currentSpeed;

    [Header("Jump")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundBox = new Vector2(0.60f, 0.08f);
    [SerializeField] private LayerMask groundMask;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private bool isDashing;
    private float dashTimer;
    private int facing = 1; 
    private int dashDir = 1;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;



    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (Mathf.Abs(moveX) > 0.01f)
            facing = moveX > 0 ? 1 : -1;

        if (sprite) sprite.flipX = facing < 0;
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
                isDashing = false;
        }

    }

    private void FixedUpdate()
    {
        if (groundCheck)
            isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBox, 0f, groundMask) != null;


        if (isDashing)
        {
            var v = rb.linearVelocity;
            v.x = dashDir * dashSpeed;   // velocidad fija de dash
            rb.linearVelocity = v;
            currentSpeed = v.x;
            return;
        }

        targetSpeed = moveX * maxSpeed;
        bool hasTarget = Mathf.Abs(targetSpeed) > 0.01f;
        float accelPerSec = hasTarget
            ? maxSpeed / (isGrounded ? accelerationTimeGround : accelerationTimeAir)
            : maxSpeed / (isGrounded ? decelerationTimeGround : decelerationTimeAir);


        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelPerSec * Time.fixedDeltaTime);


        var v2 = rb.linearVelocity;
        v2.x = currentSpeed;
        rb.linearVelocity = v2;
    }


    void OnMove(InputValue input)
    {
        var m = input.Get<Vector2>();
        moveX = Mathf.Clamp(m.x, -1f, 1f);
    }

    void OnJump(InputValue input)
    {
        if (!input.isPressed) return;
        TryJump();
    }

    void OnSprint(InputValue input)
    {
        if (!input.isPressed) return;
        TryDash();
    }
    void TryJump()
    {
        if (!isGrounded) return;

        var v = rb.linearVelocity;
        if (v.y < 0f) v.y = 0f;
        rb.linearVelocity = v;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        currentSpeed = rb.linearVelocity.x;

        animator.SetTrigger("Jump");

    }

    void TryDash()
    {
        if (isDashing) return; 


        dashDir = Mathf.Abs(moveX) > 0.01f ? (moveX > 0 ? 1 : -1) : facing;
        facing = dashDir;
        isDashing = true;
        dashTimer = dashDuration;


        var v = rb.linearVelocity;
        v.x = dashDir * dashSpeed;
        rb.linearVelocity = v;
        currentSpeed = v.x;

        animator.SetTrigger("Dash");
    }





    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;

     
        Gizmos.DrawWireCube(groundCheck.position, (Vector3)groundBox);
    }


    void OnEnable() { EnhancedTouchSupport.Enable(); TouchSimulation.Enable(); }
    void OnDisable() { TouchSimulation.Disable(); EnhancedTouchSupport.Disable(); }
}
