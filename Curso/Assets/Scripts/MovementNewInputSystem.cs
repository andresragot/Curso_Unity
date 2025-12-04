using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementNewInputSystem : MonoBehaviour
{
    public float movementSpeed = 2f;

    private Vector2 dir;
    private Vector2 velocity;
    private Rigidbody2D rb;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            string key = ctx.control?.displayName ?? ctx.control?.name ?? "Unknow";

            Debug.Log("[DOWN] " + key);
        }
        else if (ctx.canceled)
        {
            string key = ctx.control?.displayName ?? ctx.control?.name ?? "Unknow";
            Debug.Log("[UP] " + key);
        }

        dir = ctx.ReadValue<Vector2>();
        Debug.Log("Vector: " + dir);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void LateUpdate()
    {
        velocity = dir * movementSpeed;

        rb.linearVelocity = velocity;
    }
}
