using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterMovement_New : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Vector2 MovementInput;
    public float Velocity;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
        Debug.Log("Vector: " + MovementInput);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = MovementInput * Velocity * Time.deltaTime;
        if (MovementInput.x < 0)
        {
            sr.flipX = true;
        }
        else if (MovementInput.x > 0)
        {
            sr.flipX = false;
        }
    }
}
