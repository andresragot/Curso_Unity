using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private PlayerInputMovement input_movement;
    [SerializeField] private float speed = 6f;

    private Rigidbody2D rb;

    private void Awake()
    {
        if (input_movement == null)
        {
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            if (gameManager != null) Debug.Log("Obtuvimos el game manager");

            input_movement = gameManager.GetComponent<PlayerInputMovement>();

            if (input_movement != null) Debug.Log("Obtuvimos el input_movement");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 dir = input_movement ? input_movement.input : Vector2.zero;
        rb.linearVelocity = dir * speed;
    }
}
