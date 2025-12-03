using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementInput : MonoBehaviour
{
    public float movementSpeed = 2.0f;

    private Vector2 input;
    private Vector2 velocity;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); // -1 a 1
        float y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y).normalized;

        velocity = input * movementSpeed;

        rb.linearVelocity = velocity;
    }
}
