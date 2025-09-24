using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerJump2D : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask groundLayer = 0;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float grounCheckRadius = 0.15f;

    [Header("Salto")]
    [SerializeField, Tooltip("Fuerza vertical del salto")]
    private float jumpForce = 12f;
    [SerializeField, Tooltip("Fuerza del segundo salto")]
    private float jumpForceDouble = 24f;
    [SerializeField, Tooltip("Fuerza mantenida")]
    private float jumpForceHeld = 40f;

    [SerializeField, Tooltip("Tiempo Máximo de Mantenido")]
    private float maxTime = 0.5f;
    private float maxTimeAux = 0f;

    [SerializeField, Tooltip("Tiempo Coyote")]
    private float coyoteTime = 0.7f;
    private float coyoteTimeAux = 0f;

    [SerializeField, Tooltip("Buffer de salto")]
    private float jumpBufferTime = 1f;
    private float jumpBufferTimeAux = 0f;

    private bool jumpPressed;
    private bool jumpHeld;
    private bool was_jumping = false;

    private bool second_jump_available = false;

    [Header("Movimiento")]
    public Vector2 MovementInput;
    public float Velocity;

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            jumpPressed = true;
            jumpHeld = true;

            maxTimeAux = maxTime;
        }
        else if (ctx.canceled)
        {
            jumpHeld = false;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            coyoteTimeAux = coyoteTime;
            second_jump_available = false;
            if (was_jumping && rb.linearVelocityY <= 0)
            {
                anim.SetTrigger("Land");
                was_jumping = false;
            }
        }
        else
        {
            coyoteTimeAux -= Time.deltaTime;
        }

        if (jumpPressed)
        {
            jumpBufferTimeAux = jumpBufferTime;
            jumpPressed = false;
        }
        else
        {
            jumpBufferTimeAux -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (jumpBufferTimeAux > 0 && coyoteTimeAux > 0)
        {
            jump();
            second_jump_available = true;
            anim.SetTrigger("Jump");
            was_jumping = true;
            jumpBufferTimeAux = 0;
            coyoteTimeAux = 0;
        }
        else if (jumpHeld)
        {
            rb.linearVelocityY += jumpForceHeld * Time.deltaTime;
            maxTimeAux -= Time.deltaTime;

            if (maxTimeAux <= 0)
            {
                jumpHeld = false;
            }
        }
        else if (second_jump_available && jumpBufferTimeAux > 0 && coyoteTimeAux < 0)
        {
            rb.linearVelocityY = jumpForceDouble;
            anim.SetTrigger("Jump");
            second_jump_available = false;
            jumpBufferTimeAux = 0;
        }

        rb.linearVelocityX = MovementInput.x * Velocity;
        anim.SetFloat("Speed", Mathf.Abs(MovementInput.x));
        if (MovementInput.x > 0)
        {
            sr.flipX = false;
        }
        else if (MovementInput.x < 0)
        {
            sr.flipX = true;
        }
    }

    void jump()
    {
        rb.linearVelocityY = jumpForce;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, grounCheckRadius, groundLayer) != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            if (IsGrounded())
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawWireSphere(groundCheck.position, grounCheckRadius);
        }
    }
}
