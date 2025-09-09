using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

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
    [SerializeField] private float groundRadius = 0.30f;
    [SerializeField] private Vector2 groundBox = new Vector2(0.60f, 0.08f);
    [SerializeField] private LayerMask groundMask;
    //private float lastGroundedTime; // para coyote
    //private float lastJumpPressed;  // para buffer
    //private int lastJumpFrame;    // frame en que se encoló
    //private int processedJumpFrame; // frame ya procesado



    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;



    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {        
        sprite.flipX = moveX < 0f && Mathf.Abs(moveX) > 0.01f;
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
    }

    private void FixedUpdate()
    {
        if (groundCheck)
            isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBox, groundMask);
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
   



        targetSpeed = moveX * maxSpeed;
        bool hasTarget = Mathf.Abs(targetSpeed) > 0.01f;
        float accelPerSec = hasTarget
            ? maxSpeed / (isGrounded ? accelerationTimeGround : accelerationTimeAir)
            : maxSpeed / (isGrounded ? decelerationTimeGround : decelerationTimeAir);


        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelPerSec * Time.fixedDeltaTime);


        var v = rb.linearVelocity;
        v.x = currentSpeed;
        rb.linearVelocity = v;
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


    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
     
        Gizmos.DrawWireCube(groundCheck.position, (Vector3)groundBox);
    }


    void OnEnable() { EnhancedTouchSupport.Enable(); TouchSimulation.Enable(); }
    void OnDisable() { TouchSimulation.Disable(); EnhancedTouchSupport.Disable(); }
}
